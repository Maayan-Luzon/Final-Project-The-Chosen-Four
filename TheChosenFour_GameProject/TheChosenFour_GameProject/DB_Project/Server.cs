using DB_Project.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.System;
using static System.Collections.Specialized.BitVector32;

namespace DB_Project
{
    public static class Server
    {
        // data base's path in the computer
        private static string dbPath = ApplicationData.Current.LocalFolder.Path;

        // the path which the program will connect with tp the data base
        private static string connectionString = "Filename=" + dbPath + "\\GameDB.db";

        /*
          The method checks whether the user has entered correct data and is in the user database.
          If the data is correct, the operation returns the user's userId. If the data is incorrect, 
          the operation returns a null value              
         */
        public static int? ValidateUser(string userName, string userPassword)
        {
            string query = $"SELECT UserId FROM [Users] WHERE UserName='{userName}' AND UserPassword='{userPassword}'";
            using (SqliteConnection connection = new SqliteConnection(connectionString)) // יצירת אובייקט של קשר בין בסיס הנתונים לתוכנית
            {
                connection.Open(); // פתיחת הקשר
                SqliteCommand command = new SqliteCommand(query, connection); //sql יצירת פקודה של 
                SqliteDataReader reader = command.ExecuteReader(); // יצירת אובייקט עם אובייקט שמאפשר לנו לקרוא
                if (reader.HasRows) // האם יש שורות? תוכן מסוים?
                {
                    reader.Read();
                    return reader.GetInt32(0); // החזרת האיבר הראשון והיחיד שהוא ה id
                }
                return null;
            }
        }

        
        /*
         A new user is added to the database via this action.Practically speaking, the action adds information
         to the GameData and Users tables. The Users table will be updated with the user's personal
         information. Since this is the user's first game, default game data will be put to the GameData table.
         Important to emphasize: the action returns a user object that is full of data and ready to play
         */
        public static GameUser AddNewUser(string name, string password, string mail)
        {
            int? userId = ValidateUser(name, password); // בדיקה אם המשתמש כבר נמצא במאגר
            if (userId != null) // המשתמש כבר קיים - לשלוח להתחברות במקום הרשמה
                return null;
            // אם המשכנו, זאת אומרת המשתמש בעל הנתונים שהזין לא נמצא במאגר
            //User מסיפים את נתוניו האישיים של המשתמש שהזין לטבלת 
            string query = $"INSERT INTO [Users] (UserName,UserPassword,UserMail) VALUES ('{name}','{password}','{mail}')";
            Execute(query);
            userId = ValidateUser(name, password); //User של המשתמש לאחר הוספתו לטבלת UserId קבלת 
            //-------------------------------------------
            AddGameSet(userId.Value);
            AddGameData(userId.Value); //הוספת נתוני ברירת מחדל 
            //AddUserProduct(userId.Value);
            return GetUser(userId.Value);
        }

    
        /*
         A user with all of their fields filled up is returned by the action.
         In order for the user to enter the game, the action gathers data from four tables and fills them.
         The process first retrieves the table User, extracts the user's ID, name, and email address, and then
         proceeds to populate the user data. then 'SetUser', a helper action, assists the activity.
         */
        public static GameUser GetUser(int userId)
        {
            GameUser user = null;
            string query = $"SELECT UserId, UserName, UserMail FROM [Users] WHERE UserId={userId}";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    user = new GameUser
                    {
                        UserId = reader.GetInt32(0),
                        UserName = reader.GetString(1),
                        UserMail = reader.GetString(2),
                    };
                }
            }
            if (user != null)
            {
                SetUser(user);//המשך מילוי משתמש
            }
            return user; // user doesn't exsit
        }

        /*
          The user's fields are being filled up by the action. It retrieves the user's game data (MaxLevel, Money, 
          and CurrentLevelId) by accessing the table GameData, Sign in and enter Money, the MaxLevel user.
          Additionally, the action uses SetCurrentLevel to access the Level table in the CurrentLevel in order to fill 
          in the CurrentLevelId. The cause of this will be an assistive action. This information will also be added to 
          GameUser. To sum up, the information was gathered gradually from four tables and entered into the bone
          The game will now be accessible to the user.
         */
        private static void SetUser(GameUser user)
        {
            int currentLevelId = 0;
            int setId = 0;
            string query = $"SELECT CurrentLevelId, MaxLevel, Money, SetId FROM [GameData] WHERE UserId={user.UserId}";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    user.MaxLevel = reader.GetInt32(1);
                    user.Money = reader.GetInt32(2);
                    currentLevelId = reader.GetInt32(0);
                    setId = reader.GetInt32(3);
                }
            }
            SetCurrentLevel(user, currentLevelId);
            SetGameSet(user);
            //SetCurrentProduct(user, setId);

        }


        /*
 Fitcher -שולפת ממנה את שם ה currentProductId ולפי Product הפעולה מסייעת לגשת לטבלת 
 GameUser מסוג user אותו היא שמה במשתנה  
  */
        private static void SetCurrentProduct(GameUser user, int currentProductId)
        {
            string query = $"SELECT ProductName FROM [Product] WHERE ProductId={currentProductId}";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    // user.UsingProducts.Add(reader.GetString(0)); // to do it like that? 
                }
            }
        }

        /*
        In the following step, the action creates an object of type GameLevel and inserts it into the user after 
        accessing the database Level and extracting the difficulty level data based on currentLevelId.
        */
        public static void SetCurrentLevel(GameUser user, int currentLevelId)
        {
            string query = $"SELECT LevelId,LevelNumber,CountMonsters,CountLogs FROM [Level] WHERE LevelId={currentLevelId}";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    user.CurrentLevel = new GameLevel
                    {
                        LevelID = reader.GetInt32(0),
                        LevelNumber = reader.GetInt32(1),
                        CountMonsters = reader.GetInt32(2),
                    };
                }
            }
        }

        /*
          The method insert into the GameData table new line for new user with deafault values of new users. 
         */
        private static void AddGameData(int userId)
        {
            int setId = GetGameSetId(userId);
            string query = $"INSERT INTO [GameData] (UserId,CurrentLevelId,SetId,MaxLevel,Money) VALUES " +
                $"({userId}, {1}, {setId}, {1}, {0})";
            Execute(query);
        }

    
        /*
         The action adds a new line to the user product table when a purchase is made in-store.
         The line contains the product's id and the user's id and in this way the product is 
         being connected to the user.
         */
        public static void AddUserProduct(int userId, int productId = 0)
        {
            string query = $"INSERT INTO [UserProduct] (UserId, ProductId) VALUES ({userId}, {productId})";
            Execute(query);
        }


        // This method execute given query
        private static void Execute(string query)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                command.ExecuteNonQuery();
            }
        }


        //--------------------------------------------------------*********--------------------------------------------------------


        /* 
          This method get product's name and access the Product table in order to returns new Product fill with data
          from the table about the wanted product. 
        */
        public static Product GetProduct(string productName)
        {
            Product product = null;
            if (productName != null)
            {
                string query = $"SELECT * FROM [Product] WHERE ProductName='{productName}'";
                using (SqliteConnection connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    SqliteCommand command = new SqliteCommand(query, connection);
                    SqliteDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        product = new Product
                        {
                            ProductId = reader.GetInt32(0),
                            ProductPrice = reader.GetInt32(1),
                            ProductName = reader.GetString(2),
                            ProductType = reader.GetString(3),
                            ProductLives = reader.GetInt32(4),
                            ProductStrength = reader.GetInt32(5)
                        };
                    }
                }
            }
            return product;
        }

        /*
         This method returns a set of a apecific setId. It access the Set table and select all the data which belongs
        to the specific id. 
         */
        public static Set GetSetOfProducts(int setId)
        {
            Set set = null;
            string query = $"SELECT * FROM [Set] WHERE SetId={setId}";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    set = new Set
                    {
                        SetId = setId,
                        MusketeerShoe = GetProduct(reader.IsDBNull(2) ? null : reader.GetString(2)),
                        MusketeerShirt = GetProduct(reader.IsDBNull(3) ? null : reader.GetString(3)),
                        MusketeerSword = GetProduct(reader.IsDBNull(4) ? null : reader.GetString(4)),
                        MusketeerShield = GetProduct(reader.IsDBNull(5) ? null : reader.GetString(5)),
                        MusketeerBow = GetProduct(reader.IsDBNull(6) ? null : reader.GetString(6)),

                        KnightShoe = GetProduct(reader.IsDBNull(7) ? null : reader.GetString(7)),
                        KnightShirt = GetProduct(reader.IsDBNull(8) ? null : reader.GetString(8)),
                        KnightSword = GetProduct(reader.IsDBNull(9) ? null : reader.GetString(9)),
                        KnightShield = GetProduct(reader.IsDBNull(10) ? null : reader.GetString(10)),
                        KnightBow = GetProduct(reader.IsDBNull(11) ? null : reader.GetString(11)),

                        ArcherShoe = GetProduct(reader.IsDBNull(12) ? null : reader.GetString(12)),
                        ArcherShirt = GetProduct(reader.IsDBNull(13) ? null : reader.GetString(13)),
                        ArcherSword = GetProduct(reader.IsDBNull(14) ? null : reader.GetString(14)),
                        ArcherShield = GetProduct(reader.IsDBNull(15) ? null : reader.GetString(15)),
                        ArcherBow = GetProduct(reader.IsDBNull(16) ? null : reader.GetString(16)),

                        WizardShoe = GetProduct(reader.IsDBNull(17) ? null : reader.GetString(17)),
                        WizardShirt = GetProduct(reader.IsDBNull(18) ? null : reader.GetString(18)),
                        WizardSword = GetProduct(reader.IsDBNull(19) ? null : reader.GetString(19)),
                        WizardShield = GetProduct(reader.IsDBNull(20) ? null : reader.GetString(20)),
                        WizardBow = GetProduct(reader.IsDBNull(21) ? null : reader.GetString(21)),
                    };
                }
            }
            return set;
        }

        /*
         This method add new line into the Set table with a given userId. It will be used when creating new user. 
         */
        public static void AddGameSet(int userId)
        {
            string query = $"INSERT INTO [Set] (UserId) VALUES ('{userId}')";
            Execute(query);
        }

        /*
         This method returns the setId of a given usserId. 
        */
        public static int GetGameSetId(int userId)
        {
            int setId = 0;
            string query = $"SELECT SetId FROM [Set] WHERE UserId={userId}";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    setId = reader.GetInt32(0);
                }
            }
            return setId;
        }

        /*
         This method sets the user's set to be the set that apear in the Set table, for a given user.
        */
        public static void SetGameSet(GameUser user)
        {
            user.Set = GetSetOfProducts(GetGameSetId(user.UserId));
        }

        /*
         This method returns list of products which the user own. It take the information from the UserProduct table. 
        */
        public static List<int> GetOwnProductsId(GameUser gameUser)
        {
            List<int> ownProductsIds = new List<int>();
            string query = $"SELECT ProductId FROM [UserProduct] WHERE UserId={gameUser.UserId}";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ownProductsIds.Add(reader.GetInt32(0));
                    }
                    return ownProductsIds;
                }
                return null;
            }
        }

        /*
        This action add product to the set of the user. It recives user, the hero who suppose to get the product
        and the product name. 
        */
        public static void AddProductToSet(GameUser user, string heroName, string productName)
        {
            Product product = GetProduct(productName); // get the product from its name
            string productInSetName = heroName + product.ProductType; // the property's name inside Set
            Type setType = typeof(Set); // get the type of Set
            PropertyInfo productInSet = setType.GetProperty(productInSetName); // get the property using its name
            if (productInSet != null)
            {
                productInSet.SetValue(user.Set, product);
            }
        }

        /*
         At the end of each game, this method will save the new data into the GameData and Set tables. In case the user
        have not sign up to th game, his data will be deleted from the tables. 
        */
        public static void SaveChanges(GameUser user)
        {
            string query = "";
            if (user.UserId == 0)
            {
                query = $"DELETE FROM [UserProduct] WHERE UserId={0}";
                Execute(query);
            }
            else
            {

                query = $"UPDATE [GameData] SET CurrentLevelId = {user.CurrentLevel.LevelID}," +
                    $" SetId = {user.Set.SetId}, MaxLevel = {user.MaxLevel}, Money = {user.Money}" +
                    $" WHERE UserId = {user.UserId}";
                Execute(query);

                query = $"UPDATE [Set] SET " +
                    $"SetId = {user.Set.SetId}, " +
                    $"MusketeerShoe = '{(user.Set.MusketeerShoe != null ? user.Set.MusketeerShoe.ProductName : null)}', " +
                    $"MusketeerShirt = '{(user.Set.MusketeerShirt != null ? user.Set.MusketeerShirt.ProductName : null)}', " +
                    $"MusketeerSword = '{(user.Set.MusketeerSword != null ? user.Set.MusketeerSword.ProductName : null)}', " +
                    $"MusketeerShield = '{(user.Set.MusketeerShield != null ? user.Set.MusketeerShield.ProductName : null)}', " +
                    $"MusketeerBow = '{(user.Set.MusketeerBow != null ? user.Set.MusketeerBow.ProductName : null)}', " +

                    $"KnightShoe = '{(user.Set.KnightShoe != null ? user.Set.KnightShoe.ProductName : null)}', " +
                    $"KnightShirt = '{(user.Set.KnightShirt != null ? user.Set.KnightShirt.ProductName : null)}', " +
                    $"KnightSword = '{(user.Set.KnightSword != null ? user.Set.KnightSword.ProductName : null)}', " +
                    $"KnightShield = '{(user.Set.KnightShield != null ? user.Set.KnightShield.ProductName : null)}', " +
                    $"KnightBow = '{(user.Set.KnightBow != null ? user.Set.KnightBow.ProductName : null)}', " +

                    $"ArcherShoe = '{(user.Set.ArcherShoe != null ? user.Set.ArcherShoe.ProductName : null)}', " +
                    $"ArcherShirt = '{(user.Set.ArcherShirt != null ? user.Set.ArcherShirt.ProductName : null)}', " +
                    $"ArcherSword = '{(user.Set.ArcherSword != null ? user.Set.ArcherSword.ProductName : null)}', " +
                    $"ArcherShield = '{(user.Set.ArcherShield != null ? user.Set.ArcherShield.ProductName : null)}', " +
                    $"ArcherBow = '{(user.Set.ArcherBow != null ? user.Set.ArcherBow.ProductName : null)}', " +

                    $"WizardShoe = '{(user.Set.WizardShoe != null ? user.Set.WizardShoe.ProductName : null)}', " +
                    $"WizardShirt = '{(user.Set.WizardShirt != null ? user.Set.WizardShirt.ProductName : null)}', " +
                    $"WizardSword = '{(user.Set.WizardSword != null ? user.Set.WizardSword.ProductName : null)}', " +
                    $"WizardShield = '{(user.Set.WizardShield != null ? user.Set.WizardShield.ProductName : null)}', " +
                    $"WizardBow = '{(user.Set.WizardBow != null ? user.Set.WizardBow.ProductName : null)}' " +
                    $"WHERE UserId={user.UserId}";
                Execute(query);
            }
        }

        /*
         This method returns list of own products. 
        */
        public static List<Product> ListOfUsedProducts(GameUser user)
        {
            List<Product> products = new List<Product>();
            string query = $"SELECT * FROM [Set] WHERE UserId={user.UserId}";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    for (int i = 2; i < 22; i++)
                    {
                        products.Add(GetProduct(reader.GetString(i)));
                    }
                }
            }
            return products;
        }

        /*
         This method returns list of the name of the products that specific hero own from the Set table.
        */
        public static List<string> NameListOfUsedProductsHero(GameUser user, string heroName)
        {
            List<string> products = new List<string>();
            string query = $"SELECT {heroName}Shoe, {heroName}Shirt, {heroName}Sword, " +
                $"{heroName}Shield, {heroName}Bow FROM [Set] WHERE UserId={user.UserId}";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    for (int i = 2; i < 22; i++)
                    {
                        products.Add(GetProduct(reader.GetString(i)).ProductName);
                    }
                }
            }
            return products;
        }

        /*
          This method returns list of the name of the products that specific hero own from the user's Set. 
        */
        public static List<string> HeroProducts(GameUser user, string heroName)
        { 
            List<string> products = new List<string>();
            string heroShoeName = heroName + "Shoe"; // the property's name inside Set
            string heroShirtName = heroName + "Shirt";
            string heroSwordName = heroName + "Sword";
            string heroShieldName = heroName + "Shield";
            string heroBowName = heroName + "Bow";
            Type setType = typeof(Set); // get the type of Set
            PropertyInfo heroShoe = setType.GetProperty(heroShoeName); // get the property using its name
            PropertyInfo heroShirt = setType.GetProperty(heroShirtName);
            PropertyInfo heroSword = setType.GetProperty(heroSwordName);
            PropertyInfo heroShield = setType.GetProperty(heroShieldName);
            PropertyInfo heroBow = setType.GetProperty(heroBowName);
            
            if (heroShoe != null)            
                products.Add(((Product)heroShoe.GetValue(user.Set)) == null 
                    ? "" : ((Product)heroShoe.GetValue(user.Set)).ProductName);

            if (heroShirt != null)
                products.Add(((Product)heroShirt.GetValue(user.Set)) == null
                    ? "" : ((Product)heroShirt.GetValue(user.Set)).ProductName);

            if (heroSword != null)
                products.Add(((Product)heroSword.GetValue(user.Set)) == null
                    ? "" : ((Product)heroSword.GetValue(user.Set)).ProductName);

            if (heroShield != null)
                products.Add(((Product)heroShield.GetValue(user.Set)) == null 
                    ? "" : ((Product)heroShield.GetValue(user.Set)).ProductName);

            if (heroBow != null)
                products.Add(((Product)heroBow.GetValue(user.Set)) == null
                    ? "" : ((Product)heroBow.GetValue(user.Set)).ProductName);

            return products;
        }

        public static string FindPassword(string userName, string userEmail)
        {
            string query = $"SELECT UserPassword FROM [Users] WHERE UserName='{userName}' AND UserMail='{userEmail}'";
            using (SqliteConnection connection = new SqliteConnection(connectionString)) // יצירת אובייקט של קשר בין בסיס הנתונים לתוכנית
            {
                connection.Open(); // פתיחת הקשר
                SqliteCommand command = new SqliteCommand(query, connection); //sql יצירת פקודה של 
                SqliteDataReader reader = command.ExecuteReader(); // יצירת אובייקט עם אובייקט שמאפשר לנו לקרוא
                if (reader.HasRows) // האם יש שורות? תוכן מסוים?
                {
                    reader.Read();
                    return reader.GetString(0); // החזרת האיבר הראשון והיחיד שהוא ה id
                }
                return null;
            }
        }
    }

}
