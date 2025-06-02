using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EcologicCard : MonoBehaviour, ICard
{
    public IEffect Effect;
    private EcologicCradType _type = EcologicCradType.None;

    public string FieldZone = "";
    public string CardTittle;
    public string CardDescription;
    public string Clasification;

   


    
    protected void InitCard()
    {
        // switch(Type)
        // {
        //     case EcologicCradType.Action:
        //         effect = new EducacionAmbientalEffect();
        //     break;
        // }
    }

    
    public void ApplyEfect()
    {
        throw new System.NotImplementedException();
    }



    public EcologicCradType Type
    {
        get{
            return _type;
        }

        set{
            if(value == EcologicCradType.None)
                return;

            switch(value)
            {
                case EcologicCradType.EducacionAmbiental:
                    Effect = new EducacionAmbientalEffect();
                    FieldZone = "Inmediate";
                    CardTittle = "Educacion Ambiental";
                    Clasification = "Accion";
                    CardDescription = "Esta carta te permite robar 2 cartas y descartar 1 residuo";

                break;

                case EcologicCradType.EnergiaLimpia:
                    Effect = new EnergiaLimpiaEffect();
                    FieldZone = "Inmediate";
                    CardTittle = "Energia Limpia";
                    Clasification = "Accion";
                    CardDescription = "Esta carta elimina el ultimo proyecto contaminante de tu campo";




                break;

                case EcologicCradType.ReciclajeUrbano:
                    Effect = new ReciclajeUrbanoEffect();
                    FieldZone = "Inmediate";
                    CardTittle = "Reciclaje Hurbano";
                    Clasification = "Accion";
                    CardDescription = "Esta carta te libera de 2 residuos y optienes 1 material reciclado";




                break;

                case EcologicCradType.SubvencionGubernamental:
                    Effect = new SubvencionGubernamentalEffect();
                    FieldZone = "Inmediate";
                    CardTittle = "Subvencion Gubernamental";
                    Clasification = "Accion";
                    CardDescription = "Esta carta te permite optener 3 materiales reciclados";




                break;

                case EcologicCradType.ContaminacionAire:
                    Effect = new ContaminacionAireEffect();
                    FieldZone = "Challenge";
                    CardTittle = "Contaminacion del Aire";
                    Clasification = "Desafio";
                    CardDescription = "Esta carta reduce a la mitad los puntos de sostenibilidad del adversario por turno, solo puede ser anulada con una carta de ENERGIA LIMPIA";




                break;

                case EcologicCradType.ResiduosToxicos:
                    Effect = new ResiduosToxicosEffect();
                    FieldZone = "Challenge";
                    CardTittle = "Residuos Toxicos";
                    Clasification = "Desafio";
                    CardDescription = "Esta carta obliga al oponente a descartar 2 cartas aleatorias u obtener 3 residuos, en caso de no tener mas de 3 cartas, se vera obligado a obtener 3 residuos";




                break;

                case EcologicCradType.SequiaExtrema:
                    Effect = new SequiaExtremaEffect();
                    FieldZone = "Challenge";
                    CardTittle = "Sequia Extrema";
                    Clasification = "Desafio";
                    CardDescription = "Esta carta impide que los proyectos de Infraestructura de tu oponente generen puntos de sostenibilidad";




                break;

                case EcologicCradType.AlianzaGlobal:
                    Effect = new AlianzaGlobalEffect();
                    FieldZone = "Inmediate";
                    CardTittle = "Alianza Global";
                    Clasification = "Colaboracion";
                    CardDescription = "Todos obtienen 2 materiales reciclados a cambio de 10 puntos de sostenibilidad (o los restantes). Si no se tienen puntos de sostenibilidad no aplica efecto";




                break;

                case EcologicCradType.CumbreClimatica:
                    Effect = new CumbreClimaticaEffect();
                    FieldZone = "Inmediate";
                    CardTittle = "Cumbre Climatica";
                    Clasification = "Colaboracion";
                    CardDescription = "Esta carta permite que todos descartan 2 residuos y robar 1 carta. Si no se tienen residuos no aplica efecto";




                break;

                case EcologicCradType.InnovacionCompartida:
                    Effect = new InnovacionCompartidaEffec6t();
                    FieldZone = "Inmediate";
                    CardTittle = "Innovacion Compartida";
                    Clasification = "Colaboracion";
                    CardDescription = "Esta carta te permite copiar a tu campo un proyecto de Recurso del jugador contrario, si no dispone de proyectos no aplica efecto";




                break;

                case EcologicCradType.EdificioVerde:
                    Effect = new EdificioVerdeEffect();
                    FieldZone = "Permanent";
                    CardTittle = "Edificio Verde";
                    Clasification = "Infraestructura";
                    CardDescription = "Esta carta te permite generar 1 punto de sostenibilidad/turno y descartar 1 residuo/turno. Requiere 8 de materia prima para jugar";




                break;

                case EcologicCradType.ParqueEolico:
                    Effect = new ParqueEolicoEffect();
                    FieldZone = "Permanent";
                    CardTittle = "Parque Eolico";
                    Clasification = "Infraestructura";
                    CardDescription = "Esta carta te permite obtener 2 puntos de sostenibilidad/turno. Requiere 12 de materia prima para jugar";




                break;

                case EcologicCradType.TransporteElectrico:
                    Effect = new TransporteElectricoEffect();
                    FieldZone = "Permanent";
                    CardTittle = "Transporte Electrico";
                    Clasification = "Infraestructura";
                    CardDescription = "Esta carta te permite obtener 3 puntos de sostenibilidad/turno. Requiere 18 de materia prima para jugar";




                break;

                case EcologicCradType.CentroReciclaje:
                    Effect = new CentrodeReciclajeEffect();
                    FieldZone = "Temporal";
                    CardTittle = "Centro de Reciclaje";
                    Clasification = "Recursos";
                    CardDescription = "Esta carta te permite obtener 2 puntos de sostenibilidad/turno, descartar 1 residuo/turno y obtener 1 material reciclado/turno. Permanece activa 3 turnos";




                break;

                case EcologicCradType.GranjaSolar:
                    Effect = new GranjaSolarEffect();
                    FieldZone = "Temporal"; //2turnos
                    CardTittle = "Ganja Solar";
                    Clasification = "Recursos";
                    CardDescription = "Esta carta te permite obtener 2 puntos de sostenibilidad/turno. Permanece activa 4 turnos";




                break;

                case EcologicCradType.HuertoUrbano:
                    Effect = new HuertoUrbanoEffect();
                    FieldZone = "Temporal"; //2turnos
                    CardTittle = "Huerto Urbano";
                    Clasification = "Recursos";
                    CardDescription = "Esta carta te permite obtener 1 puntos de sostenibilidad/turno y descartar 1 residuo/turno. Permanece activa 5 turnos";




                break;

                case EcologicCradType.None:
                    Effect = null;
                break;

            }

            _type = value;
        }
    }

    public void SetCard(EcologicCradType value, Card card)
    {


        switch(value)
        {
            case EcologicCradType.EducacionAmbiental:
                    

            break;

            case EcologicCradType.EnergiaLimpia:
                 

            break;

            case EcologicCradType.ReciclajeUrbano:
                    

            break;

            case EcologicCradType.SubvencionGubernamental:
                  

            break;

            case EcologicCradType.ContaminacionAire:
                ((ContaminacionAireEffect)Effect).card = card;

            break;

            case EcologicCradType.ResiduosToxicos:
                ((ResiduosToxicosEffect)Effect).card = card;

            break;

            case EcologicCradType.SequiaExtrema:
                ((SequiaExtremaEffect)Effect).card = card;

            break;

            case EcologicCradType.AlianzaGlobal:
                    

            break;

            case EcologicCradType.CumbreClimatica:
                    

            break;

            case EcologicCradType.InnovacionCompartida:
                    

            break;

            case EcologicCradType.EdificioVerde:
                    

            break;

            case EcologicCradType.ParqueEolico:
                    

            break;

            case EcologicCradType.TransporteElectrico:
                    

            break;

            case EcologicCradType.CentroReciclaje:
                ((CentrodeReciclajeEffect)Effect).card = card;
            break;

            case EcologicCradType.GranjaSolar:
                ((GranjaSolarEffect)Effect).card = card;

            break;

            case EcologicCradType.HuertoUrbano:
                ((HuertoUrbanoEffect)Effect).card = card;

            break;

            case EcologicCradType.None:
                   
            break;

        }
    }
}
