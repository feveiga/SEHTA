package com.example.dto;
import java.util.ArrayList;
import java.util.List;

//import org.mvel2.util.Make.List;

import com.fasterxml.jackson.annotation.JsonProperty;

import lombok.Data;

@Data
public class EtiologiaResp {
    @JsonProperty("etiologia")
    private String etiologia = "PRIMARIA"; //primaria-secundaria

    @JsonProperty("cushing") 
    private boolean cushing = false;

    @JsonProperty("feocromocitoma")
    private boolean feocromocitoma = false;

    @JsonProperty("hiperaldosteronismo")
    private boolean hiperaldosteronismo = false;

    @JsonProperty("disfuncion_tiroidea")
    private boolean disfuncion_tiroidea = false;
    
    @JsonProperty("acromegalia")
    private boolean acromegalia = false;

    @JsonProperty("sahos")
    private boolean sahos = false;

    @JsonProperty("hta_nefrogenica")
    private boolean hta_nefrogenica = false;
    
    @JsonProperty("sindrome_carncinoide")
    private boolean  sindrome_carncinoide = false;

    @JsonProperty("sust_efecto_presor")
    private boolean sust_efecto_presor = false;

    @JsonProperty("hta_renovascular")
    private boolean hta_renovascular = false;

    @JsonProperty("coartacion_aorta")
    private boolean coartacion_aorta = false;
    
    @JsonProperty("reglas")
    private List<String> lisReglas =  new ArrayList<>();

}
