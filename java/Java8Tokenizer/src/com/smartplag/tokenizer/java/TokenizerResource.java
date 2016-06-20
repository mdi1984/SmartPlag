package com.smartplag.tokenizer.java;

import java.io.StringReader;

import javax.ws.rs.GET;
import javax.ws.rs.Path;
import javax.ws.rs.Produces;
import javax.ws.rs.core.Response;
import javax.ws.rs.core.Response.Status;

import org.antlr.v4.runtime.ANTLRInputStream;
import org.antlr.v4.runtime.CharStream;
import org.antlr.v4.runtime.CommonTokenStream;
import org.antlr.v4.runtime.TokenSource;
import org.antlr.v4.runtime.TokenStream;

import com.smartplag.tokenizer.Java8Lexer;
import com.smartplag.tokenizer.Java8Parser;

@Path("/tokenizer")
public class TokenizerResource {

	@GET
	@Produces("application/json")
	public Response Test() {
		String sampleSource = "public class RunnableTest { "+
                "  public static void main(String[] args) { "+
                "    System.out.println(\"=== RunnableTest ===\"); "+
                "    // Anonymous Runnable  "+
                "    Runnable r1 = new Runnable(){  "+
                "      @Override  "+
                "      public void run(){  "+
                "        System.out.println(\"Hello world one!\");  "+
                "      }  "+
                "    };  "+
                "    // Lambda Runnable  "+
                "    Runnable r2 = () -> System.out.println(\"Hello world two!\");  "+
                "    // Run em!  "+
                "    r1.run();  "+
                "    r2.run();  "+
                "  } "+
                "} ";
		
		try {

			CharStream inputCharStream = new ANTLRInputStream(new StringReader(sampleSource));
			TokenSource tokenSource = new Java8Lexer(inputCharStream);
			TokenStream inputTokenStream = new CommonTokenStream(tokenSource);
			Java8Parser parser = new Java8Parser(inputTokenStream);
		} catch (Exception ex) {
			
		}
		// parser.addErrorListener(new TestErrorListener());

//		ProgramContext context = parser.program();

//		logger.info(context.toString());

		return Response.status(Status.OK).entity(sampleSource).build();
	}
}
