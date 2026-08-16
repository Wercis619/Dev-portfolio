package game;

import java.util.ArrayList;
import java.util.Random;
import java.util.Scanner;

public class PomocnikGry {
    private static final String ALFABET = "abcdefg";
    private static final int PLANSZA_DLUGOSC = 7;
    private static final int PLANSZA_WIELKOSC = 49;
    private static final int MAX_PROB = 200;
    static final int INKREMENTACJA_W_POZ = 1;
    static final int INKREMENTACJA_W_PION = PLANSZA_DLUGOSC;

    private final int [] plansza = new int[PLANSZA_WIELKOSC];
    private final Random liczbaLosowa = new Random();
    private int liczbaStartupow = 0;

    public String pobierzDaneWejsciowe(String komunikat) {
        System.out.print(komunikat + ": ");
        Scanner skaner = new Scanner(System.in);
        return skaner.nextLine().toLowerCase();
    }

    public ArrayList<String> rozmiescStartup(int wielkoscStartupu) {

        int[] wspolrzedneStartupu = new int[wielkoscStartupu];
        int proby = 0;
        boolean czySieUdalo = false;
        liczbaStartupow++;
        int wartoscInkrementacji = getWartoscInkrementacji();
        while (!czySieUdalo & proby++ < MAX_PROB) {
            int polozenie = liczbaLosowa.nextInt(PLANSZA_WIELKOSC);
            for (int i = 0; i < wspolrzedneStartupu.length; i++) {
                wspolrzedneStartupu[i] = polozenie;
                polozenie += wartoscInkrementacji;
            }

            if (czyStartupPasuje(wspolrzedneStartupu, wartoscInkrementacji)) {
                czySieUdalo = czyWspolrzedneDostepne(wspolrzedneStartupu);
            }
        }

        zapiszPolozenieNaPlanszy(wspolrzedneStartupu);
        ArrayList<String> komorkiAlfaNum = konwertujPolozenieDoFormatuAlfaNum(wspolrzedneStartupu);
        return komorkiAlfaNum;
    }

    private boolean czyStartupPasuje(int[] wspolrzedneStartupu, int wartInkrementacji) {
        int koncowePolozenie = wspolrzedneStartupu[wspolrzedneStartupu.length - 1];
        if (wartInkrementacji == INKREMENTACJA_W_POZ) {
            return obliczWierszZIndeksu(wspolrzedneStartupu[0]) == obliczWierszZIndeksu(koncowePolozenie);
        } else {
            return koncowePolozenie < PLANSZA_WIELKOSC;
        }
    }

    private boolean czyWspolrzedneDostepne(int[] wspolrzedneStartupu) {
        for (int wspolrzedna : wspolrzedneStartupu) {
            if (plansza[wspolrzedna] != 0) {
                return false;
            }
        }
        return true;
    }

    private void zapiszPolozenieNaPlanszy(int[] wspolrzedneStartupu) {
        for (int indeks : wspolrzedneStartupu) {
            plansza[indeks] = 1;
        }
    }

    private ArrayList<String> konwertujPolozenieDoFormatuAlfaNum(int[] wspolrzedneStartupu) {
        ArrayList<String> komorkiAlfaNum = new ArrayList<String>();
        for (int indeks : wspolrzedneStartupu) {
            String wspolrzedneAlfaNum = pobierzWspolrzedneAlfaNumNaPdstwIndeksu(indeks);
            komorkiAlfaNum.add(wspolrzedneAlfaNum);
        }
        return komorkiAlfaNum;
    }

    private String pobierzWspolrzedneAlfaNumNaPdstwIndeksu(int indeks) {
        int wiersz = obliczWierszZIndeksu(indeks);
        int kolumna = indeks % PLANSZA_DLUGOSC;
        String litera = ALFABET.substring(kolumna, kolumna + 1);
        return litera + wiersz;
    }

    private int obliczWierszZIndeksu(int indeks) {
        return indeks / PLANSZA_DLUGOSC;
    }

    private int getWartoscInkrementacji() {
        if (liczbaStartupow % 2 == 0) {
            return INKREMENTACJA_W_POZ;
        } else {
            return INKREMENTACJA_W_PION;
        }
    }

}
