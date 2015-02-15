using elektronische_componenten.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace elektronische_componenten.DAL
{
    public class DatabaseInitializer : DropCreateDatabaseAlways<ComponentContext>
    {
        protected override void Seed(ComponentContext context)
        {
            base.Seed(context);

            Categorie c1 = new Categorie("Connectoren");
            Categorie c2 = new Categorie("Kabels");
            Categorie c3 = new Categorie("Sensoren");
            Categorie c4 = new Categorie("Schakelaars");

            context.Categoriën.Add(c1);
            context.Categoriën.Add(c2);
            context.Categoriën.Add(c3);
            context.Categoriën.Add(c4);
            context.SaveChanges();

            context.Componenten.Add(new Component("Modulairstekker Stekker", c1,
                "http://www.conrad.be/ce/nl/product/738597/Modulairstekker-Stekker-recht-RJ12-Aantal-polen-6P6C-P-128-Transparant-Lumberg-Inhoud-1-stuks/?ref=category&rt=category&rb=1",
                1, 0.25));

            context.Componenten.Add(new Component("USB-stekker voor zelfbouw Type A", c1,
                "http://www.conrad.be/ce/nl/product/747013/BKL-Electronic-10120098-USB-stekker-voor-zelfbouw-Type-A-A-USBPA-N-Stekker-recht-1-stuks?ref=list",
                4, 0.81));

            context.Componenten.Add(new Component("ABL Sursum Geaarde rubberen stekker", c1,
                "http://www.conrad.be/ce/nl/product/552646/ABL-Sursum-Geaarde-rubberen-stekker-zwart/?ref=category&rt=category&rb=1",
                2, 2.99));

            context.Componenten.Add(new Component("Aansluitkabel CAT 5e U/UTP 2 m Grijs", c2,
                "http://www.conrad.be/ce/nl/product/990439/Goobay-Netwerk-Aansluitkabel-CAT-5e-UUTP-2-m-Grijs/?ref=category&rt=category&rb=1",
                7, 1.99));

            context.Componenten.Add(new Component("Druksensor Interlink FSR-402 ± 10 g - 10 kg", c2,
                "http://www.conrad.be/ce/nl/product/503369/Druksensor-Interlink-FSR-402-10-g-10-kg?ref=list",
                3, 9.44));

            context.Componenten.Add(new Component("Afstandsensor VDM28-8-L-IO/73c/110/122", c3,
                "http://www.conrad.be/ce/nl/product/156449/Afstandsensor-VDM28-8-L-IO73c110122?ref=list",
                1, 370.78));

            context.Componenten.Add(new Component("Afstandsensor VDM28-8-L-IO/73c/110/122", c3,
                "http://www.conrad.be/ce/nl/product/156449/Afstandsensor-VDM28-8-L-IO73c110122?ref=list",
                1, 370.78));

            context.Componenten.Add(new Component("drukschakelaar 250 V/DC 3 A 1x aan/aan IP67", c4,
                "http://www.conrad.be/ce/nl/product/701778/LAS2GQF-11ZER12VNP-Vandalismebestendige-drukschakelaar-250-VDC-3-A-1x-aanaan-IP67-vergrendelend-1-stuks/?ref=category&rt=category&rb=1",
                6, 10.99));

            context.Componenten.Add(new Component("KN3 Tuimelschakelaar 250 V/AC", c4,
                "http://www.conrad.be/ce/nl/product/701211/KN3-Tuimelschakelaar-250-VAC-3-A-2x-aanuitaan-vergrendelend0vergrendelend-1-stuks/?ref=category&rt=category&rb=1",
                4, 3.00));

            context.SaveChanges();
        }
    }
}