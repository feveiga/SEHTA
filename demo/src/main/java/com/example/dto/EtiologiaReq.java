package com.example.dto;
import com.fasterxml.jackson.annotation.JsonProperty;
import lombok.Data;

@Data
public class EtiologiaReq {

//#region CUSHING
    @JsonProperty("supresion_dexametasona")
    private float supresion_dexametasona = 0;

    @JsonProperty("cortisol_libre")
    private float cortisol_libre = 0;

    @JsonProperty("cortisol_nocturno")
    private float cortisol_nocturno = 0;
//#endregion

//#region HIPERPARATIROIDISMO se determina por dos o mas de estos 
    @JsonProperty("calcio")
    private float calcio = 0;

    @JsonProperty("pth")
    private float pth = 0;
    //#endregion

//#region FEOCROMOCITOMA
// 1 o mas sintomas determinan feocromocitoma
    @JsonProperty("norepinefrina_sangre")
    private float norepinefrina_sangre = 0; //ok

   @JsonProperty("dopamina_sangre")
    private float dopamina_sangre = 0;  //ok

    @JsonProperty("epinefrina_sangre")
    private float epinefrina_sangre = 0; //ok

    @JsonProperty("epinefrina_orina")
    private float epinefrina_orina = 0; //ok

    @JsonProperty("norepinefrina_orina")
    private float norepinefrina_orina = 0; //ok

    @JsonProperty("metanefrina_orina")
    private float metanefrina_orina = 0; //ok

    @JsonProperty("acido_vainillin_mandelico")
    private float acido_vainillin_mandelico = 0; //ok
//#endregion
//#region Disfuncion tiroidea
    @JsonProperty("tsh")
    private float tsh = 1;

    @JsonProperty("t4")
    private float t4 = 15;
//#endregion disfuncion tiroidea

//#region acromegalia
    @JsonProperty("igf1")
    private float igf1 = 0;

    @JsonProperty("edad")
    private float edad = 0;
//#endregion

//#region sindrome_carncinoide
    @JsonProperty("acido_hidroxindolacetico")
    private float acido_hidroxindolacetico = 0;

    @JsonProperty("serotonina")
    private float serotonina = 0;

    @JsonProperty("cromogranina")
    private float cromogranina = 0;
//#endregion

//#region HTA Nefrogenica
    @JsonProperty("quistes")
    private Integer quistes = 0;

    @JsonProperty("ant_poliquistosis_renal")
    private boolean ant_poliquistosis_renal = false;

    @JsonProperty("proteinuria")
    private Integer proteinuria = 0;
    
    @JsonProperty("tfg")
    private Integer tfg = 100;

//#endregion

//#region SUST EFECTO PRESOR
    @JsonProperty("consumo_otras_sustancias")
    private boolean consumo_otras_sustancias = false;

    @JsonProperty("consumo_farmacos")
    private boolean consumo_farmacos = false;

    @JsonProperty("consumo_drogas")
    private boolean consumo_drogas = false;

//#endregion

//#region HIPERALDOSTERONISMO / 2 0 MAS SINTOMAS
    @JsonProperty("sodio")
    private float sodio = 0;

    @JsonProperty("concentracion_aldosterona")
    private float concentracion_aldosterona = 0;

    @JsonProperty("arp")
    private float arp = 0;
//#endregion

//#region SAHOS
    @JsonProperty("indice_apnea_hipopnea")
    private Integer indice_apnea_hipopnea = 0;

//#endregion

//#region Coartacion Aorta

    @JsonProperty("coartacion")
    private boolean coartacion = false;
//#endregion Coartacion Aorta

//#region HTA Renovascular
    @JsonProperty("estenosis_renal")
    private boolean estenosis_renal = false;

//#endregion HTA Renovascular
}


