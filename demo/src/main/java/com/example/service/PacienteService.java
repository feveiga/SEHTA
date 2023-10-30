package com.example.service;
//import java.util.Collection;

//import org.kie.api.KieBase;
//import org.kie.api.event.rule.AgendaEventListener;
import org.kie.api.runtime.KieContainer;
import org.kie.api.runtime.KieSession;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
//import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import com.example.controller.PacienteController;
import com.example.dto.*;
import lombok.RequiredArgsConstructor;

//import org.kie.api.definition.rule.Rule;


@Service
@RequiredArgsConstructor
public class PacienteService {
    private final KieContainer kieContainer;
    private static final Logger logger = LoggerFactory.getLogger(PacienteController.class);
    

    public PacienteResp pacienteResponse2(PacienteReq pacienteReq)
    {
        logger.info("pacienteReq: {}", pacienteReq);
        PacienteResp pacienteResp = new PacienteResp();
        logger.info("PacienteResp: {}", pacienteResp);
        KieSession kieSession = kieContainer.newKieSession();
        kieSession.setGlobal("pacienteResp2", pacienteResp); //identifier debe coincidir con la variable global definida en .drl
        kieSession.insert(pacienteReq);
        kieSession.fireAllRules();
        kieSession.dispose();
        return pacienteResp;
    }

    //determina causa hipertension
    public EtiologiaResp etiologia(EtiologiaReq etiologiaReq)
    {
        logger.info("Etiologia-Request: {}", etiologiaReq);
        EtiologiaResp etiologiaResp = new EtiologiaResp();
        logger.info("Etiologia-Response: {}", etiologiaResp);
        KieSession kieSession = kieContainer.newKieSession();
        kieSession.setGlobal("etiologiaResp", etiologiaResp); //identifier debe coincidir con la variable global definida en .drl
        kieSession.insert(etiologiaReq);
        kieSession.fireAllRules();
        kieSession.dispose();
        
        return etiologiaResp;
    }
  
}
