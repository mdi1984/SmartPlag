package com.smartplag.tokenizer.java;

import javax.ws.rs.GET;
import javax.ws.rs.Path;
import javax.ws.rs.Produces;
import javax.ws.rs.core.Response;
import javax.ws.rs.core.Response.Status;

@Path("/tokenizer")
public class TokenizerResource {

	@GET
	@Produces("application/json")
	public Response Test() {
		return Response.status(Status.OK).entity("test").build();
	}
}
