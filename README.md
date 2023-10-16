# Автоматизация продажи билетов в кинотеат
## Схема моделей

```mermaid
    classDiagram
    Ticket <|-- Hall
    Ticket <|-- Cinema
    Ticket <|-- Client
    Ticket <|-- Film
    Ticket <|-- Staff
    Staff .. Post
    class Hall{
        +Guid Id
        +short Number
        +short NumberOfSeats
    }
    class Cinema{
        +Guid Id
        +string Title
        +string Address
    }
    class Client {
        +Guid Id
        +string LastName
        +string FirstName
        +string Patronymic
        +short Age
        +string? Email
    }
    class Film {
        +Guid Id
        +string Title
        +string Description
        +short Limitation
    }

    class Staff {
        +Guid Id
        +string LastName
        +string FirstName
        +string Patronymic
        +short Age
        +Post Post
    }
    class Ticket {
        +Guid Id 
        +Guid HallId 
        +Guid CinemaId
        +Guid FilmId
        +Guid ClientId
        +Guid StaffId
        +short Row
        +short Place
        +decimal Price
        +DateTimeOffset Date
    }
    class Post{
        Cashier(Кассир)
        Manager(Менеджер)
    }

```
