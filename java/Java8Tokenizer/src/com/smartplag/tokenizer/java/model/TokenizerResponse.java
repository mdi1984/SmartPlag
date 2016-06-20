package com.smartplag.tokenizer.java.model;

import java.util.List;

import com.fasterxml.jackson.annotation.JsonProperty;

public class TokenizerResponse {
	@JsonProperty("Title")
	private String title;

	@JsonProperty("StudentResults")
	private List<StudentResult> studentResults;

	public String getTitle() {
		return title;
	}

	public void setTitle(String title) {
		this.title = title;
	}

	public List<StudentResult> getStudentResults() {
		return studentResults;
	}

	public void setStudentResults(List<StudentResult> studentResults) {
		this.studentResults = studentResults;
	}

}
