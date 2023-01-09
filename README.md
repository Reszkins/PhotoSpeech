# PhotoSpeech

## Cel projektu

### Problem
Podczas nauki języków obcych często zaniedbywanym obszarem jest wymowa. Jest to dziedzina szczególnie trudna, ze względu na konieczność wejścia w dialog z lektorem, który musi w takim wypadku poświęcić znaczącą ilość swojego czasu uczniowi. 

Innym problematycznym aspektem tego zagadnienia jest monotonna forma przyswajania wiedzy – użytkownicy szybko nużą się nauką słówek z podręczników czy fiszek. 

### Rozwiązanie

Aplikacja służąca do pomocy w nauce języków poprzez głosowe odgadywanie treści obrazków. 

Użytkownicy mogą zagrać w grę, w której wyświetlane są losowe obrazki z internetu (według ustalonych kategorii i nazwy) lub predefiniowane (tworzone przez moderatorów). Celem użytkownika jest wypowiedzenie na głos odgadywanego hasła w danym języku.  

W przypadku poprawnej odpowiedzi użytkownik otrzymuje punkt, w przypadku braku znajomości rozwiązania – pomija zdjęcie. Po zakończonej rozgrywce wyświetlane są punkty i poprawne odpowiedzi. 

Dostępny jest ranking użytkowników, w którym wyświetlane są punkty za najszybsze odpowiedzi. 

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

## Blob storage
W blob storage znajdują się kontenery odpowiadające słowom w bazie.  
W kotenerach znajdują się obrazki które są losowane podczas grania w grę.
