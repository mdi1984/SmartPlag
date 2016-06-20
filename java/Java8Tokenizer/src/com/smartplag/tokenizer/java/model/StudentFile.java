package com.smartplag.tokenizer.java.model;

import com.fasterxml.jackson.annotation.JsonProperty;

public class StudentFile {
	@JsonProperty("FileName")
	private String fileName;
	@JsonProperty("Base64Source")
	private String base64Source;

	public String getFileName() {
		return fileName;
	}

	public void setFileName(String fileName) {
		this.fileName = fileName;
	}

	public String getBase64Source() {
		return base64Source;
	}

	public void setBase64Source(String base64Source) {
		this.base64Source = base64Source;
	}

}
