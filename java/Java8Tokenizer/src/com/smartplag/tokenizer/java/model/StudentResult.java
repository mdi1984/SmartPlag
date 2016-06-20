package com.smartplag.tokenizer.java.model;

import java.util.List;

import com.fasterxml.jackson.annotation.JsonProperty;

public class StudentResult {
	@JsonProperty("FirstName")
	private String firstName;
	@JsonProperty("LastName")
	private String lastName;
	@JsonProperty("Files")
	private List<TokenMap> files;

	public String getFirstName() {
		return firstName;
	}

	public void setFirstName(String firstName) {
		this.firstName = firstName;
	}

	public String getLastName() {
		return lastName;
	}

	public void setLastName(String lastName) {
		this.lastName = lastName;
	}

	public List<TokenMap> getFiles() {
		return files;
	}

	public void setFiles(List<TokenMap> files) {
		this.files = files;
	}

}
