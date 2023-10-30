package com.example.controller;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import com.example.service.PacienteService;
import com.example.dto.*;

@RestController
public class PacienteController {
    private final PacienteService pacienteService;

    public PacienteController(PacienteService pacienteService)
    {
        this.pacienteService = pacienteService;
    }

    private static final Logger logger = LoggerFactory.getLogger(PacienteController.class);


    @RequestMapping("/pa")
    public ResponseEntity<Object> getPaciente2(@RequestBody PacienteReq pacienteReq2)
    {
        logger.info("Recibiendo solicitud con pacienteRequest: {}", pacienteReq2);
        PacienteResp pacienteResp = pacienteService.pacienteResponse2(pacienteReq2);
        logger.info("Saliendo de Recibir solicitud");
        return new ResponseEntity<>(pacienteResp, HttpStatus.OK);
    }

     @RequestMapping("/etiologia")
    public ResponseEntity<Object> determinaEtiologia(@RequestBody EtiologiaReq etiologiaReq)
    {
        logger.info("Recibiendo solicitud con pacienteRequest: {}", etiologiaReq);
        EtiologiaResp etiologiaResp = pacienteService.etiologia(etiologiaReq);
        logger.info("Saliendo de Recibir solicitud");
        return new ResponseEntity<>(etiologiaResp, HttpStatus.OK);
    }

     @RequestMapping("/despedir")
        public String despedir(){
        return "chau";
    }
}
