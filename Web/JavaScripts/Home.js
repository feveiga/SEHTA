"use strict"
function diccionario(e) {
    textAreaRespuesta.value = e;
}

function guardarDatosOrinaDB() {
    var js_orina = JSON.stringify({
        supresion_dexametasona: supresion_dexametasona.value,
        cortisol_libre: cortisol_libre.value,
        tfg: tfg.value,
        proteinuria: proteinuria.value,
        epinefrina_orina: epinefrina_urinaria.value,
        norepinefrina_orina: norepinefrina_urinaria.value,
        metanefrina_orina: metanefrina.value,
        acido_vainillin_mandelico: acido_vainillin_mandelico.value,
        acido_hidroxindolacetico: acido_hidroxindolacetico.value,
        sodio: sodio.value
    });
    $.ajax({
        type: "POST",
        url: '@Url.Action("agregarDatosOrina", "Home")',
        dataType: "json",
        data: js_orina,
        contentType: "application/json; charset=utf-8",
        success: function (respuesta) {
            alert(respuesta);
        }
    });
}

function guardarDatosSangreDB() {
    var js_sangre = JSON.stringify({
        arp: arp.value,
        concentracion_aldosterona: concentracion_aldosterona.value,
        cromogranina: cromogranina_a.value,
        dopamina_sangre: dopamina.value,
        epinefrina_sangre: epinefrina.value,
        igf1: igf1.value,
        norepinefrina_sangre: norepinefrina.value,
        serotonina: serotonina.value,
        t4: t4.value,
        tsh: tsh.value,
    });
    $.ajax({
        type: "POST",
        url: '@Url.Action("agregarDatosSangre", "Home")',
        dataType: "json",
        data: js_sangre,
        contentType: "application/json; charset=utf-8",
        success: function (respuesta) {
            alert(respuesta);
        }
    });
}

function guardarEstudiosVariosDB() {
    alert(cortisol_nocturno.value);
    var js_estudios_varios = JSON.stringify({
        cortisol_nocturno: cortisol_nocturno.value,
        coartacion: coartacion_si.checked,
        estenosis_renal: estenosis_renal_si.checked,
        quistes: quistes_rinon.value,
        indice_apnea_hipopnea: iah.value
    });
    $.ajax({
        type: "POST",
        url: '@Url.Action("agregarOtrosEstudios", "Home")',
        dataType: "json",
        data: js_estudios_varios,
        contentType: "application/json; charset=utf-8",
        success: function (respuesta) {
            alert(respuesta);
        }
    });
}

function guardarAnamnesisDB() {
    var js_anamnesis = JSON.stringify({
        ant_poliquistosis_renal: ant_poliquitosis_si.checked,
        consumo_drogas: consumo_drogas_si.checked,
        consumo_otras_sustancias: consumo_otras_sustancias_si.checked,
        consumo_farmacos: consumo_farmacos_si.checked,
    });
    $.ajax({
        type: "POST",
        url: '@Url.Action("agregarAnamnesis", "Home")',
        dataType: "json",
        data: js_anamnesis,
        contentType: "application/json; charset=utf-8",
        success: function (respuesta) {
            alert(respuesta);
        }
    });
}

function main() {
    var inputSistolica = document.getElementById("inputSistolica");
    var inputDiastolica = document.getElementById("inputDiastolica");
    var textAreaRespuesta = document.getElementById("textAreaRespuesta");
    var fechaNacimiento = document.getElementById("fechaNacimiento");
    var edad = document.getElementById("edad");
    var dni = document.getElementById("dni");
    var nombre = document.getElementById("nombre");
    var apellido = document.getElementById("apellido");
    var btnEtiologia = document.getElementById("btnEtiologia");

    //Elementos de la seccion diagnostico




    //window.addEventListener('load', main, false);
    fechaNacimiento.addEventListener('change', calcularEdad, false);


    //cushing:
    var supresion_dexametasona = document.getElementById("supresion_dexametasona");
    var cortisol_libre = document.getElementById("cortisol_libre");
    var cortisol_nocturno = document.getElementById("cortisol_nocturno");

    //disfuncion tiroidea
    var tsh = document.getElementById("tsh");
    var t4 = document.getElementById("t4");

    //acromegalia
    var igf1 = document.getElementById("igf1");

    //sahos
    var iah = document.getElementById("iah");

    //coartacion
    var coartacion_si = document.getElementById("coartacion_si");
    var coartacion_no = document.getElementById("coartacion_no");

    //hta nefrogenica
    var tfg = document.getElementById("tfg");
    var quistes_rinon = document.getElementById("quistes_rinon");
    var ant_poliquitosis_si = document.getElementById("ant_poliquitosis_si");
    var proteinuria = document.getElementById("proteinuria");

    //HTA_renovascular
    var estenosis_renal_si = document.getElementById("estenosis_renal_si");

    //Feocromocitoma
    var dopamina = document.getElementById("dopamina");
    var epinefrina = document.getElementById("epinefrina");
    var norepinefrina = document.getElementById("norepinefrina");
    var acido_vainillin_mandelico = document.getElementById("acido_vainillin_mandelico");
    var epinefrina_urinaria = document.getElementById("epinefrina_urinaria");
    var metanefrina = document.getElementById("metanefrina");
    var norepinefrina_urinaria = document.getElementById("norepinefrina_urinaria");

    //Uso sust efecto presor
    var consumo_farmacos_si = document.getElementById("consumo_farmacos_si");
    var consumo_otras_sustancias_si = document.getElementById("consumo_otras_sustancias_si");
    var consumo_drogas_si = document.getElementById("consumo_drogas_si");

    //Sindrome Carcinoide
    var acido_hidroxindolacetico = document.getElementById("acido_hidroxindolacetico");
    var cromogranina_a = document.getElementById("cromogranina_a");
    var serotonina = document.getElementById("serotonina");

    //hiperaldosteronismo
    var sodio = document.getElementById("sodio");
    var concentracion_aldosterona = document.getElementById("concentracion_aldosterona");
    var arp = document.getElementById("arp");

    inicializarTest();

}


function inicializarTest() {
    inputSistolica.value = 165;
    inputDiastolica.value = 101;
    edad.value = 62;

    //cushing
    supresion_dexametasona.value = 0.5;
    cortisol_libre.value = 10;
    cortisol_nocturno.value = 0.1;

    //disfuncion tiroidea
    tsh.value = 3;
    t4.value = 15;

    //acromegalia
    //edad.value = 60;
    igf1.value = 180;

    //sahos
    iah.value = 0;

    //HTA NEfrogenica
    tfg.value = 100;
    quistes_rinon.value = 0;
    proteinuria.value = 79;

    //Feocromocitoma
    dopamina.value = 28;
    epinefrina.value = 130;
    norepinefrina.value = 75;
    acido_vainillin_mandelico.value = 5;
    epinefrina_urinaria.value = 50;
    metanefrina.value = 0.8;
    norepinefrina_urinaria.value = 50;

    //carcionide
    acido_hidroxindolacetico.value = 7;
    cromogranina_a.value = 200;
    serotonina.value = 20;

    //hiperaldosteronismo
    sodio.value = 110;
    concentracion_aldosterona.value = 15;
    arp.value = 1.5;
}

function calcularPA() {
    var inputPresionArterial = document.getElementById("presionArterial");
    textAreaRespuesta.value = "";

    guardarDatosOrinaDB();
    guardarDatosSangreDB();
    guardarEstudiosVariosDB();
    guardarAnamnesisDB();

}



function calcularNivelPA() {
    var js_presion_arterial = JSON.stringify({ sistolica: inputSistolica.value, diastolica: inputDiastolica.value });

    $.ajax({
        type: "POST",
        url: '@Url.Action("calcularPA", "Home")',
        dataType: "json",
        data: js_presion_arterial,
        contentType: "application/json; charset=utf-8",
        success: function (respuesta) {
            var resHta = "";
            if (respuesta['presionArterial'].includes("HTA")) {
                resHta = (respuesta['presionArterial'] == "HTA1") ? "HTA-1" :
                    (respuesta['presionArterial'] == "HTA2") ? "HTA-2" :
                        "HTA-3";

                textAreaRespuesta.value += "Presion Arterial: " + resHta + "\n";
                btnEtiologia.enabled = true;
                inputPresionArterial.value = resHta;
            }

            else {
                //textAreaRespuesta.value += "IF FALSE";
                btnEtiologia.disabled = true;
            }

            //activarRB(respuesta['presionArterial']);
        }
    });
    //Guardo los datos del paciente
    var js_persona = JSON.stringify({ dni: dni.value, edad: edad.value, nombre: nombre.value, apellido: apellido.value });

    $.ajax({
        type: "POST",
        url: '@Url.Action("agregarPersonaDB", "Home")',
        dataType: "json",
        data: js_persona,
        contentType: "application/json; charset=utf-8",
        success: function (respuesta) {
            alert(respuesta);
        }
    });
    //Guardo datos de presion arterial
    var js_presion_arterial = JSON.stringify({ sistolica: inputSistolica.value, diastolica: inputDiastolica.value });

    $.ajax({
        type: "POST",
        url: '@Url.Action("agregarPresionArterial", "Home")',
        dataType: "json",
        data: js_presion_arterial,
        contentType: "application/json; charset=utf-8",
        success: function (respuesta) {
            alert(respuesta);
        }
    });
}

function determinarEtiologia() {
    var textAreaCausasSecundarias = document.getElementById("causasSecundarias");
    var inputEtiologia = document.getElementById("etiologia");
    var js_variables_clinicas = JSON.stringify({
        supresion_dexametasona: supresion_dexametasona.value, cortisol_libre: cortisol_libre.value, cortisol_nocturno: cortisol_nocturno.value,
        tsh: tsh.value, t4: t4.value,
        edad: edad.value, igf1: igf1.value,
        indice_apnea_hipopnea: iah.value,
        coartacion: coartacion_si.checked,
        tfg: tfg.value,
        quistes: quistes_rinon.value,
        proteinuria: proteinuria.value,
        ant_poliquistosis_renal: ant_poliquitosis_si.checked,
        estenosis_renal: estenosis_renal_si.checked,
        norepinefrina_sangre: norepinefrina.value,
        dopamina_sangre: dopamina.value,
        epinefrina_sangre: epinefrina.value,
        epinefrina_orina: epinefrina_urinaria.value,
        norepinefrina_orina: norepinefrina_urinaria.value,
        metanefrina_orina: metanefrina.value,
        acido_vainillin_mandelico: acido_vainillin_mandelico.value,
        consumo_drogas: consumo_drogas_si.checked,
        consumo_otras_sustancias: consumo_otras_sustancias_si.checked,
        consumo_farmacos: consumo_farmacos_si.checked,
        acido_hidroxindolacetico: acido_hidroxindolacetico.value,
        serotonina: serotonina.value,
        cromogranina: cromogranina_a.value,
        sodio: sodio.value,
        concentracion_aldosterona: concentracion_aldosterona.value,
        arp: arp.value
    });


    $.ajax({
        type: "POST",
        url: '@Url.Action("determinarEtiologia", "Home")',
        dataType: "json",
        data: js_variables_clinicas,
        contentType: "application/json; charset=utf-8",
        success: function (respuesta) {
            var res = "No";
            textAreaCausasSecundarias.value = "";

            textAreaRespuesta.value += "Etiologia: " + respuesta['etiologia'] + "\n";
            inputEtiologia.value = respuesta['etiologia'];
            textAreaRespuesta.value += "Causas:" + "\n";

            res = (respuesta['acromegalia'] == true) ? "Si" : "No";
            textAreaCausasSecundarias.value += (respuesta['acromegalia'] == true) ? "Acromegalia \n" : "";
            textAreaRespuesta.value += "Acromegalia: " + res + "\n";

            res = (respuesta['coartacion_aorta'] == true) ? "Si" : "No";
            textAreaCausasSecundarias.value += (respuesta['coartacion_aorta'] == true) ? "Coartación Aorta \n" : "";
            textAreaRespuesta.value += "Coartación Aorta: " + res + "\n";

            res = (respuesta['disfuncion_tiroidea'] == true) ? "Si" : "No";
            textAreaCausasSecundarias.value += (respuesta['disfuncion_tiroidea'] == true) ? "Disfunción Tiroidea \n" : "";
            textAreaRespuesta.value += "Disfunción Tiroidea: " + res + "\n";

            res = (respuesta['feocromocitoma'] == true) ? "Si" : "No";
            textAreaCausasSecundarias.value += (respuesta['feocromocitoma'] == true) ? "Feocromocitoma \n" : "";
            textAreaRespuesta.value += "Feocromocitoma: " + res + "\n";

            res = (respuesta['hiperaldosteronismo'] == true) ? "Si" : "No";
            textAreaCausasSecundarias.value += (respuesta['hiperaldosteronismo'] == true) ? "Hiperaldosteronismo \n" : "";
            textAreaRespuesta.value += "Hiperaldosteronismo: " + res + "\n";

            res = (respuesta['hta_nefrogenica'] == true) ? "Si" : "No";
            textAreaCausasSecundarias.value += (respuesta['hta_nefrogenica'] == true) ? "HTA Nefrogénica \n" : "";
            textAreaRespuesta.value += "HTA Nefrogénica: " + res + "\n";

            res = (respuesta['hta_renovascular'] == true) ? "Si" : "No";
            textAreaCausasSecundarias.value += (respuesta['hta_renovascular'] == true) ? "HTA Renovascular \n" : "";
            textAreaRespuesta.value += "HTA Renovascular: " + res + "\n";

            res = (respuesta['sahos'] == true) ? "Si" : "No";
            textAreaCausasSecundarias.value += (respuesta['sahos'] == true) ? "SAHOS \n" : "";
            textAreaRespuesta.value += "SAHOS: " + res + "\n";

            res = (respuesta['sindrome_carncinoide'] == true) ? "Si" : "No";
            textAreaCausasSecundarias.value += (respuesta['sindrome_carncinoide'] == true) ? "Sindrome Carcinoide \n" : "";
            textAreaRespuesta.value += "Sindrome Carcinoide: " + res + "\n";

            res = (respuesta['cushing'] == true) ? "Si" : "No";
            textAreaCausasSecundarias.value += (respuesta['cushing'] == true) ? "Sindrome de Cushing \n" : "";
            textAreaRespuesta.value += "Sindrome de Cushing: " + res + "\n";

            res = (respuesta['sust_efecto_presor'] == true) ? "Si" : "No";
            textAreaCausasSecundarias.value += (respuesta['sust_efecto_presor'] == true) ? "Sustancia efecto presor \n" : "";
            textAreaRespuesta.value += "Sustancia efecto presor: " + res + "\n";

        }
    });


    // Analisis orina






    console.log("FIN FUNCION determinarEtiologia()");

}

function calcularEdad() {
    var fnac = fechaNacimiento.value;
    var fechaNacimientoObj = new Date(fnac);
    var fechaActual = new Date();
    var diferenciaMilisegundos = fechaActual - fechaNacimientoObj;
    var anios = Math.floor(diferenciaMilisegundos / (1000 * 60 * 60 * 24 * 365.25));
    edad.value = anios;
}

window.addEventListener('load', main, false);