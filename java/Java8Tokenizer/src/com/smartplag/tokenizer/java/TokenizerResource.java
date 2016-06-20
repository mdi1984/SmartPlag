package com.smartplag.tokenizer.java;

import java.io.IOException;
import java.io.StringReader;
import java.util.ArrayList;
import java.util.List;

import javax.ws.rs.Consumes;
import javax.ws.rs.POST;
import javax.ws.rs.Path;
import javax.ws.rs.Produces;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;
import javax.ws.rs.core.Response.Status;

import org.antlr.v4.runtime.ANTLRInputStream;
import org.antlr.v4.runtime.CharStream;
import org.antlr.v4.runtime.CommonTokenStream;
import org.antlr.v4.runtime.Token;
import org.antlr.v4.runtime.TokenSource;
import org.jboss.resteasy.util.Base64;

import com.smartplag.tokenizer.Java8Lexer;
import com.smartplag.tokenizer.java.model.StudentAssignment;
import com.smartplag.tokenizer.java.model.StudentFile;
import com.smartplag.tokenizer.java.model.StudentResult;
import com.smartplag.tokenizer.java.model.TokenMap;
import com.smartplag.tokenizer.java.model.TokenWithPosition;
import com.smartplag.tokenizer.java.model.TokenizerRequest;
import com.smartplag.tokenizer.java.model.TokenizerResponse;

@Path("/tokenizer")
public class TokenizerResource {

	@POST
	@Produces(MediaType.APPLICATION_JSON)
	@Consumes(MediaType.APPLICATION_JSON)
	public Response Tokenizer(TokenizerRequest req) {
		TokenizerResponse response = new TokenizerResponse();
		if (req != null) {
			response.setTitle(req.getTitle());
			List<StudentResult> studentResults = new ArrayList<StudentResult>();
			try {
				for (StudentAssignment assignment : req.getAssignments()) {
					StudentResult studentResult = new StudentResult();
					studentResult.setFirstName(assignment.getFirstName());
					studentResult.setLastName(assignment.getLastName());
					List<TokenMap> tokenMaps = new ArrayList<TokenMap>();
					
					for (StudentFile file : assignment.getFiles()) {
						TokenMap tokenMap = new TokenMap();
						tokenMap.setFileName(file.getFileName());
						List<TokenWithPosition> tokens = new ArrayList<TokenWithPosition>();
						
						String decodedSource = new String(Base64.decode(file.getBase64Source()));

						CharStream inputCharStream = new ANTLRInputStream(new StringReader(decodedSource));
						TokenSource tokenSource = new Java8Lexer(inputCharStream);
						CommonTokenStream inputTokenStream = new CommonTokenStream(tokenSource);
						inputTokenStream.fill();
						List<Token> jTokens = inputTokenStream.getTokens();

						for (Token token : jTokens) {
							tokens.add(new TokenWithPosition(token.getStartIndex(), token.getStopIndex(), token.getType()));
							System.out.println(token);
						}
						
						tokenMap.setTokens(tokens);
						tokenMaps.add(tokenMap);
					}
					studentResult.setFiles(tokenMaps);
					studentResults.add(studentResult);
				}
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			
			response.setStudentResults(studentResults);
		}

		return Response.status(Status.OK).entity(response).build();
	}
}
