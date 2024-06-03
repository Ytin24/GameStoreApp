using System.Collections.Generic;

public class Game {
    public int ID { get; set; }
    public string NameGame { get; set; }
    public decimal Price { get; set; }
    public ICollection<LibraryGame> LibraryGames { get; set; } = new List<LibraryGame>();
    public ICollection<GameCategory> GameCategories { get; set; }
}


// Models/Category.cs
public class Category {
    public int ID { get; set; }
    public string NameCategory { get; set; }
    public ICollection<GameCategory> GameCategories { get; set; }
}

// Models/GameCategory.cs
public class GameCategory {
    public int ID { get; set; }
    public int IDGame { get; set; }
    public Game Game { get; set; }
    public int IDCategory { get; set; }
    public Category Category { get; set; }
}


// Models/User.cs
public class User {
    public int ID { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public Library Library { get; set; }
    public Profile Profile { get; set; }
}


// Models/Gender.cs
public class Gender {
    public int ID { get; set; }
    public string NameGender { get; set; }
    public ICollection<Profile> Profiles { get; set; }
}

// Models/Profile.cs
public class Profile {
    public int ID { get; set; }
    public string FName { get; set; }
    public string LName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int IDGender { get; set; }
    public Gender Gender { get; set; }
    public int IDUser { get; set; }
    public User User { get; set; }
}


// Models/Library.cs
public class Library {
    public int ID { get; set; }
    public int IDUser { get; set; }
    public User User { get; set; }
    public ICollection<LibraryGame> LibraryGames { get; set; } = new List<LibraryGame>();
}

// Models/LibraryGame.cs
public class LibraryGame {
    public int LibraryID { get; set; }
    public Library Library { get; set; }
    public int GameID { get; set; }
    public Game Game { get; set; }
}
