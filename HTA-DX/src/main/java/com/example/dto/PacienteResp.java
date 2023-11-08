package com.example.dto;
import java.util.ArrayList;
import java.util.List;

import com.fasterxml.jackson.annotation.JsonProperty;
import lombok.Data;

@Data
public class PacienteResp {
        @JsonProperty("presionArterial")
        private String presionArterial = "INDEFINIDO";

        @JsonProperty("reglas")
        private List<String> lisReglas =  new ArrayList<>();
}
