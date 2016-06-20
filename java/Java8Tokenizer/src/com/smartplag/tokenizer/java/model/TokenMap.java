package com.smartplag.tokenizer.java.model;

import java.util.List;

import com.fasterxml.jackson.annotation.JsonProperty;

public class TokenMap {
	@JsonProperty("FileName")
	private String fileName;
	@JsonProperty("Tokens")
	private List<TokenWithPosition> tokens;
	public String getFileName() {
		return fileName;
	}
	public void setFileName(String fileName) {
		this.fileName = fileName;
	}
	public List<TokenWithPosition> getTokens() {
		return tokens;
	}
	public void setTokens(List<TokenWithPosition> tokens) {
		this.tokens = tokens;
	}


}
