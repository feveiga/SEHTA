package com.example.dto;

import com.fasterxml.jackson.annotation.JsonProperty;

import lombok.Data;

@Data
public class PacienteReq {
    
    @JsonProperty("sistolica")
    private Integer sistolica;

    @JsonProperty("diastolica")
    private Integer diastolica;

}
