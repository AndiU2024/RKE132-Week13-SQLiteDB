﻿using System.Data.SQLite;

ReadData(CreateConnection());
//InsertCustomer(CreateConnection());
RemoveCustomer(CreateConnection());

static SQLiteConnection CreateConnection()
{
    SQLiteConnection connection = new SQLiteConnection("Data Source=mydb.db; Version = 3; New = True; Compress = True;");

    try
    {
        connection.Open();
        Console.WriteLine("DB found.");
    }
    catch
    {
        Console.WriteLine("DB not found.");
    }

    return connection;
}

static void ReadData(SQLiteConnection myConnection)
{
    Console.Clear();
    SQLiteDataReader reader;
    SQLiteCommand command;

    command = myConnection.CreateCommand();
    command.CommandText = "SELECT * FROM customer";

    reader = command.ExecuteReader();

    while (reader.Read())
    {
        string readerStringFirstName = reader.GetString(0);
        string readerStringLastName = reader.GetString(1);
        string readerStringDoB = reader.GetString(2);

        Console.WriteLine($"Full name: {readerStringFirstName} {readerStringLastName}; DoB: {readerStringDoB}");
    }

    myConnection.Close();
}

static void InsertCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;
    String fName, lName, dob;

    Console.WriteLine("Enter First name:");
    fName = Console.ReadLine();
    Console.WriteLine("Enter Last name");
    lName = Console.ReadLine();
    Console.WriteLine("Enter date of birth (mm-dd-yyyy)");
    dob = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"INSERT INTO customer(firstName, lastName, dateOfBirth" +
        $"VALUES ('{fName}', '{lName}', '{dob}')";

    int rowInserted = command.ExecuteNonQuery();
    Console.WriteLine($"Row Inserted: {rowInserted}");

    ReadData(myConnection);
}

static void RemoveCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;

    string idToDelete;
    Console.WriteLine("Enter an id to delete a customer:");
    idToDelete = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"DELETE FROM customer Where rowid = {idToDelete}";
    int rowRemoved = command.ExecuteNonQuery();
    Console.WriteLine($"{rowRemoved} was removed from the table customer.");

    ReadData(myConnection);
}