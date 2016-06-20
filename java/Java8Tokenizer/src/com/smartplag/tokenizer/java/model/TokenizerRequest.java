package com.smartplag.tokenizer.java.model;

import java.util.List;

import com.fasterxml.jackson.annotation.JsonProperty;

public class TokenizerRequest {
	@JsonProperty("Title")
	private String title;
	@JsonProperty("Assignments")
	private List<StudentAssignment> assignments;

	public String getTitle() {
		return title;
	}

	public void setTitle(String title) {
		this.title = title;
	}

	public List<StudentAssignment> getAssignments() {
		return assignments;
	}

	public void setAssignments(List<StudentAssignment> assignments) {
		this.assignments = assignments;
	}
}
