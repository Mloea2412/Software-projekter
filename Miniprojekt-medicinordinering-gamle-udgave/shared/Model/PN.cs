namespace shared.Model;

public class PN : Ordination
{
    public double antalEnheder { get; set; }
    public List<Dato> dates { get; set; } = new List<Dato>();

    public PN(DateTime startDen, DateTime slutDen, double antalEnheder, Laegemiddel laegemiddel) : base(laegemiddel, startDen, slutDen)
    {
        this.antalEnheder = antalEnheder;
    }

    public PN() : base(null!, new DateTime(), new DateTime())
    {
    }

    /// <summary>
    /// Registrerer at der er givet en dosis på dagen givesDen
    /// Returnerer true hvis givesDen er inden for ordinationens gyldighedsperiode og datoen huskes
    /// Returner false ellers og datoen givesDen ignoreres
    /// </summary>
    public bool givDosis(Dato givesDen)
    {
        if (givesDen.dato <= slutDen && givesDen.dato >= startDen)
        {

            dates.Add(givesDen);
            return true;
        }
        return false;
    }


    public override double doegnDosis()
    {
        //(antal gange ordinationen er anvendt * antal enheder) / (antal dage mellem første og sidste givning)
        double sum = 0;
        if (dates.Count() > 0)
        {

            DateTime min = dates.First().dato;
            DateTime max = dates.First().dato;


            // Skal skrives om så det passer til vores tilfælde
            foreach (var d in dates)
            {

                if (d.dato < min)
                {
                    min = d.dato;
                }

                if (d.dato < max)
                {
                    max = d.dato;
                }
                
            }
            int dage = (int)(max - min).TotalDays + 1;
            sum = samletDosis() / dage; 
            
        
        }
            return sum;


        /* Skal bruges i en eller anden form      
        
        { 
                    TimeSpan diff = SlutDen.Date - StartDen.Date;
                    return diff.Days + 1;
                }*/





    }
    public override double samletDosis()
    {
        return dates.Count() * antalEnheder;
    }

    public int getAntalGangeGivet()
    {
        return dates.Count();
    }

    public override String getType()
    {
        return "PN";
    }
}
