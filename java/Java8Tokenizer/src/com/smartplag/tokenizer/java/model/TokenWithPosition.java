package com.smartplag.tokenizer.java.model;

import com.fasterxml.jackson.annotation.JsonProperty;

public class TokenWithPosition {
	@JsonProperty("From")
	private int from;
	@JsonProperty("To")
	private int to;
	@JsonProperty("Token")
	private int token;

	public TokenWithPosition(int from, int to, int token) {
		super();
		this.from = from;
		this.to = to;
		this.token = token;
	}

	public int getFrom() {
		return from;
	}

	public void setFrom(int from) {
		this.from = from;
	}

	public int getTo() {
		return to;
	}

	public void setTo(int to) {
		this.to = to;
	}

	public int getToken() {
		return token;
	}

	public void setToken(int token) {
		this.token = token;
	}
}
