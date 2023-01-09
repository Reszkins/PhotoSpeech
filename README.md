# PhotoSpeech

https://photospeech.azurewebsites.net

## Cel projektu

### Problem
Podczas nauki języków obcych często zaniedbywanym obszarem jest wymowa. Jest to dziedzina szczególnie trudna, ze względu na konieczność wejścia w dialog z lektorem, który musi w takim wypadku poświęcić znaczącą ilość swojego czasu uczniowi. 

Innym problematycznym aspektem tego zagadnienia jest monotonna forma przyswajania wiedzy – użytkownicy szybko nużą się nauką słówek z podręczników czy fiszek. 

### Rozwiązanie

Aplikacja służąca do pomocy w nauce języków poprzez głosowe odgadywanie treści obrazków. 

Użytkownicy mogą zagrać w grę, w której wyświetlane są losowe obrazki z internetu (według ustalonych kategorii i nazwy) lub predefiniowane (tworzone przez moderatorów). Celem użytkownika jest wypowiedzenie na głos odgadywanego hasła w danym języku.  

W przypadku poprawnej odpowiedzi użytkownik otrzymuje punkt, w przypadku braku znajomości rozwiązania – pomija zdjęcie. Po zakończonej rozgrywce wyświetlane są punkty i poprawne odpowiedzi. 

Dostępny jest ranking użytkowników, w którym wyświetlane są punkty za najszybsze odpowiedzi. 

## Schemat działania

Schemat działania aplikacji przebiega następująco:
1. Użytkownik loguje się bądź zakłada konto.
2. System wyświetla listę dostępnych języków.
3. Użytkownik wybiera język, w którym chce odpowiadać.
4. System wyświetla listę dostępnych kategorii
5. Użytkownik wybiera pożądaną kategorię.
6. System wyświetla ekran startowy.
7. Użytkownik wybiera rozpoczęcie gry.
8. System rozpoczyna odliczanie czasu, nasłuchiwanie użytkownika i wyświetla pierwsze zdjęcie.
9. System wyświetla kolejne obrazki, aż do momentu upłynięcia czasu.
    * Jeśli użytkownik odpowie dobrze, to system pokazuje kolejny obrazek i dodaje punkty użytkownikowi.
    * Jeśli użytkownik odpowie źle, to system wyświetla komunikat o niepoprawnej odpowiedzi i dalej czeka na poprawną odpowiedź. W przypadku pięciu błędnych odpowiedzi, system wyświetli podpowiedź.
    * Jeśli użytkownik wybierze “Restart”, to gra rozpoczyna się od kroku 8.
10. System kończy rozgrywkę i wyświetla komunikat wraz z wynikiem. Zapisuje w bazie danych uzyskany rezultat, jeśli jest lepszy niż poprzedni wynik uzyskany przez użytkownika.
11. Użytkownik wybiera, czy chce grać dalej, wrócić do menu, lub czy chce zobaczyć globalny ranking.


## Architektura rozwiązania
![architekturapng](https://user-images.githubusercontent.com/73696833/210354594-879d10ec-3674-48cf-b260-dddb854fd65d.png)

## Baza danych
Użyta baza danych to Azure SQL Database.

Diagram bazy:

![diagram](https://user-images.githubusercontent.com/73696833/211349359-28876cff-6ef0-4664-8007-24b5b8a58925.png)

W bazie znajdują się tabele:
* Words, gdzie przechowywane są słowa po angielsku z przypisanymi im kategoriami
* Categories, są to kategorie słów (np. zwierzęta, jedzenie)
* Users, gdzie są przechowywane nazwy użytkowników i zahashowane hasła.
* Scores, gdzie przechowywane są wszystkie wyniki użytkowników liczone na podstawie tego ile słów udało się wypowiedzieć podczas jednej rozgrywki

## App service
App service został wykorzystany do deployu naszej aplikacji w azurewebsites. Dzięki CD po każdym commicie na gałąź master strona zostaje budowana na nowo i dostępna jest najnowsza wersja

## Blob storage
W blob storage znajdują się kontenery odpowiadające słowom w bazie.  
W kotenerach znajdują się obrazki które są losowane podczas grania w grę.

## Bing search
W czasie rozgrywki aplikacja losuje zdjęcia dla ustalonych pojęć. Czasem bierze obrazki z bloba, a czasem wyszukuje je za pomocą bing searcha. W ten sposób jesteśmy w stanie zapewnić większą różnorodność w trakcie nauki.

## KeyVault
Wszystkie klucze i connection stringi, są przechowywane w Azure Key Vaulcie za pomocą secretów. Z KeyVaultem łączymy się za pomocą stworzonego w app service, managed identity, które jest częścią Azure AD. W KeyVaulcie jest dodane policy, które pozwala naszemu App Service mieć dostęp do secretów.

## Azure AD
Wykorzystane do połączenia App service z KeyVault.

## Cognitive services

### Speech to Text
Speech to Text to część serwisu służąca do zamiany mowy na tekst. W aplikacji została wykorzystana jedna z wersji dedykowana do użycia w przeglądarce użytkownika.

Posłużono się w tym celu JavaScript SDK, które zostało wdrożone w Blazorze. Przed rozpoczęciem gry zostaje przekazana konfiguracja do SDK, która zawiera specjalny kod z informacją o nasłuchiwanym języku. Użytkownik dodatkowo musi wyrazić zgodę na użycie mikrofonu w swojej przeglądarce.

Po uruchomieniu nasłuchiwania, aplikacja kliencka w przeglądarce użytkownika zbiera otrzymywany głos z mikrofonu i odsyła go do serwisu Azure. Tam wykonywane jest szybkie przetwarzanie i program otrzymuje wynik, który jest weryfikowany. Jeśli odczyt pokrywa się z oczekiwanym rezultatem, to użytkownik otrzymuje punkt i pojawia się następny obrazek. Wówczas zmienia się również oczekiwany rezultat i użytkownik ponownie musi poprawnie odpowiedzieć na pytanie. 

Z użyciem Speech to Text wiąże się też kilka wad. Przykładowo jest wyczuwalne opóźnienie pomiędzy wypowiadanymi słowami, a otrzymywanym wynikiem zamiany mowy na tekst. Jednakże opóźnienie nie jest na tyle duże by sprawiało dyskomfort użytkownikowi. Sporą zaletą jest prostota SDK. Programista nie musi przejmować się w jaki sposób dźwięk jest przechwytywany i wysyłany. Wystarczy kierować się krótką i treściwą instrukcją przygotowaną przez firmę Microsoft.

### Translator

Translator został wykorzystany do tłumaczenia słówek z bazy danych na wybrany przez użytkownika język.

Słówka w bazie danych znajdują się w języku angielskim. Na początku rozgrywki słówka te są pobierane z bazy na podstawie wybranej przez użytkownika kategorii, a następnie są tłumaczone za pomocą Translatora na wybrany język. W trakcie gry porównywane są słówka już przetłumaczone ze słowami wypowiadanymi przez użytkownika.

Komunikacja programu z serwisem odbywa się poprzez wysłanie odpowiedniego zapytania zawierającego kod języka tłumaczonego i kod języka oczekiwanego.

## Azure Functions

Azure Functions zostały wykorzystane do stworzenia czasowo wyzwalanej funckji, która czyści punkty użytkowników w bazie danych, co określony czas.

## Azure DevOps

W czasie trwania projektu został wykorzystany Azure DevOps do zarządzania projektem. Najczęściej używana była tablica Kanban, która pozwoliłla na przydzielenie zadań dla konkretnych użytkowników, a także na oznaczanie statusu zadań. Tabela w trakcie prac prezentowała się następująco:

![image](https://user-images.githubusercontent.com/57834846/211391512-3e031497-a039-4686-b7fb-bc64945ed897.png)


## Stos technologiczny

W projekcie zostały użyte następujące technologie: 
 - .NET 6 
 - Blazor


