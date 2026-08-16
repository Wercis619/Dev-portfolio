package game;
import java.util.ArrayList;

public class StartupGraMax {
    private PomocnikGry pomocnik = new PomocnikGry();
    private ArrayList<Startup> startupy = new ArrayList<Startup>();
    private int liczbaRuchow = 0;
    private void przygotujGre(){
        Startup pierwszy = new Startup();
        pierwszy.setNazwa("orion");
        Startup drugi = new Startup();
        drugi.setNazwa("flowberg");
        Startup trzeci = new Startup();
        trzeci.setNazwa("napferyn");
        startupy.add(pierwszy);
        startupy.add(drugi);
        startupy.add(trzeci);

        System.out.println("Twoim celem jest zatopienie 3 startupów.");
        System.out.println("orion, flowberg, napiferyn");
        System.out.println("Postaraj sie je zatopić, wykonując jak najmniej ruchów.");

        for (Startup startup : startupy){
            ArrayList<String> nowePolozenie = pomocnik.rozmiescStartup(3);
            startup.setPolaPolozenia(nowePolozenie);
        }
    }
    private void rozpocznijGre(){
        while (!startupy.isEmpty()){
            String ruchGracza = pomocnik.pobierzDaneWejsciowe("Podaj pole:");
            sprawdzRuchGracza(ruchGracza);
        }
        zakonczGre();
    }
    private void sprawdzRuchGracza(String ruchGracza){
        liczbaRuchow++;
        String wynik = "pudlo";

        for (Startup startupDoSprawdzenia : startupy){
            wynik = startupDoSprawdzenia.sprawdz(ruchGracza);
            if (wynik.equals("trafiony")){
                break;
            }
            if (wynik.equals("zatopiony")){
                startupy.remove(startupDoSprawdzenia);
                break;
            }
        }
        System.out.println(wynik);
    }
    private void zakonczGre(){
        System.out.println("Wszystkie startupy zostały zatopione!");
        if (liczbaRuchow<=18){
            System.out.println("Wykonałeś jedynie " + liczbaRuchow + " ruchów");
        } else{
            System.out.println("Ale sie guzdrałeś! Wykonałeś aż " + liczbaRuchow + " ruchów");
        }
    }
    public static void main (String[] args){
        StartupGraMax gra = new StartupGraMax();
        gra.przygotujGre();
        gra.rozpocznijGre();
    }
}
