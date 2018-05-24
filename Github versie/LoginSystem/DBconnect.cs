using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace LoginSystem
{
    class DBconnect
    {
        /*static void Main(string[] args)
        {

            
            Console.WriteLine("type login or Create Account");
            string input = Console.ReadLine();
            if (input == "Create Account")
            {
                ///Create_Account();
            }
            else
            {
                Console.WriteLine("Failed");
            }
            



        Create_Account();
            //Create_Melding();
        }
        */

        // Data Checker, kijkt of de email al gebruikt word
        static public bool CheckForAvailableEmail(List<string> dbEmailList, string email)
        {
            foreach (string dbEmail in dbEmailList)
            {
                if (dbEmail.Trim().ToLower().Equals(email.ToLower()))
                {
                    return false;
                }
            }
            return true;
        }


        //Leest de database data
        static public DataTable GrabData(string statement)
        {

            DataTable dataStore = new DataTable();
            SqlConnection connection = new SqlConnection("Data Source=145.24.222.151,8080;" +
                                                         "Database=E-medic;Persist Security Info=True;" +
                                                         "User ID=sa; Password=Teemo1999");

            SqlCommand sqlStatement = new SqlCommand(statement, connection);
            SqlDataAdapter dataGrabber = new SqlDataAdapter(sqlStatement);


            connection.Open();
            dataGrabber.Fill(dataStore);
            connection.Close();

            return dataStore;
        }



        // Create een account en geeft een Bool terug of de email bezet is
        static public void Create_Account()
        {

            Console.WriteLine("Lets get ourselfs some data!...");
            Console.WriteLine();

            DataTable data = GrabData("select * from  Gebruiker");
            int counter = 0;

            var emailList = new List<string>();
            foreach (DataRow row in data.Rows)
            {
                counter++;
                //Console.WriteLine(counter + ". " + row[0] + row[1] + row[2] + row[3] + row[4] + row[5] + row[6] + row[7]);

                //var Naam = row[0];
                var EmailObject = row[1];
                if (EmailObject != null)
                {
                    emailList.Add(EmailObject.ToString());
                }
                //var wachtwoord = row[2];
                //var achternaam = row[3];
                //var bloedtype = row[4];
                //var leeftijd = row[5];
                //var geboortedatum = row[6];
                //var aandoeing = row[7];
                var Data = Tuple.Create(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7]);

                //var SubwijkGeweldInstantie = new SubwijkGeweld(subwijk, geweldtotaal, geweldprom);
                //x.Add(subwijk)

                //Console.WriteLine(Data);
                Console.ReadLine();
            }

            Console.WriteLine("MAILS:");
            foreach (string mail in emailList)
            {
                Console.WriteLine(" - " + mail);
            }


            Console.WriteLine("Insert name: ");
            String naam = Console.ReadLine();

            Console.WriteLine("Insert email: ");
            String email = Console.ReadLine();

            Console.WriteLine("Insert wachtwoord: ");
            String wachtwoord = Console.ReadLine();

            Console.WriteLine("Insert achternaam: ");
            String achternaam = Console.ReadLine();

            Console.WriteLine("Insert bloedgroep: ");
            String bloedgroep = Console.ReadLine();

            Console.WriteLine("Insert leeftijd: ");
            int leeftijd = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Insert geboortedatum: ");
            String geboortedatum = Console.ReadLine();

            Console.WriteLine("Insert Aandoening: ");
            String Aandoening = Console.ReadLine();

            bool available = CheckForAvailableEmail(emailList, email);

            //string Email;
            //Email = data.Rows[1].ToString;

            Console.WriteLine("Email: " + email + "'s availability: " + available);



            //if (Checking(data.Rows[1].ToString(), email) == true)
            //{
            //    //PushDataGebruiker(naam, email, wachtwoord, achternaam, bloedgroep, leeftijd, geboortedatum, Aandoeing);
            //    Console.WriteLine(data.Rows[1].ToString());
            //    Console.WriteLine(email);
            //    Console.WriteLine(Checking((data.Rows[1].ToString()), email));
            //    Console.ReadLine();
            //}
            //else
            //{
            //    Console.WriteLine("failed");
            //}

            Console.ReadLine();

        }
        //Create een melding
        static public void Create_Melding()
        {
            Console.WriteLine("Insert tijd: ");
            String tijd = Console.ReadLine();

            Console.WriteLine("Insert datum: ");
            String datum = Console.ReadLine();

            Console.WriteLine("Insert naam: ");
            String naam = Console.ReadLine();

            Console.WriteLine("Insert email: ");
            String email = Console.ReadLine();

            PushDataMedeling(tijd, datum, naam, email);
        }

        //Pushed de data van Create_Account naar de database
        static public void PushDataGebruiker(string naam, string email, string wachtwoord, string achternaam, string bloedtype, int leeftijd, string geboortedatum, string ziekte)
        {

            DataTable dataStore = new DataTable();

            SqlConnection connection = new SqlConnection("Data Source=145.24.222.151,8080;" +
                                                         "Database=E-medic;Persist Security Info=True;" +
                                                         "User ID=sa; Password=Teemo1999");





            SqlCommand cmd = new SqlCommand("Insert into Gebruiker(naam,email,wachtwoord,achternaam,bloedtype,leeftijd,geboortedatum,ziekte)values(@naam,@email,@wachtwoord,@achternaam,@bloedtype,@leeftijd,@geboortedatum,@ziekte)", connection);
            Console.Write("Entering the Id's.....");
            cmd.Parameters.Add("@naam", SqlDbType.Char, 30).Value = naam;
            cmd.Parameters.Add("@email", SqlDbType.VarChar, 45).Value = email;
            cmd.Parameters.Add("@wachtwoord", SqlDbType.VarChar, 200).Value = wachtwoord;
            cmd.Parameters.Add("@achternaam", SqlDbType.Char, 45).Value = achternaam;
            cmd.Parameters.Add("@bloedtype", SqlDbType.Char, 30).Value = bloedtype;
            cmd.Parameters.Add("@leeftijd", SqlDbType.Int).Value = leeftijd;
            cmd.Parameters.Add("@geboortedatum", SqlDbType.Date).Value = geboortedatum;
            cmd.Parameters.Add("@ziekte", SqlDbType.VarChar, 200).Value = ziekte;

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                Console.WriteLine("Record Inserted Successfully");
            }
            else
            {
                Console.WriteLine("Operation Failed,Please Try Again Later");
            }
            connection.Close();


        }
        static public void PushDataMedeling(string tijd, string datum, string naam, string email)
        {

            DataTable dataStore = new DataTable();

            SqlConnection connection = new SqlConnection("Data Source=145.24.222.151,8080;" +
                                                         "Database=E-medic;Persist Security Info=True;" +
                                                         "User ID=sa; Password=Teemo1999");





            SqlCommand cmd = new SqlCommand("Insert into Medeling(tijd,datum,naam,email)values(@tijd,@datum,@naam,@email)", connection);
            Console.Write("Entering the Id's:");
            cmd.Parameters.Add("@tijd", SqlDbType.Time).Value = tijd;
            cmd.Parameters.Add("@datum", SqlDbType.Date).Value = datum;
            cmd.Parameters.Add("@naam", SqlDbType.VarChar, 45).Value = naam;
            cmd.Parameters.Add("@email", SqlDbType.VarChar, 45).Value = email;


            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                Console.WriteLine("Record Inserted Successfully");
            }
            else
            {
                Console.WriteLine("Operation Failed,Please Try Again Later");
            }
            connection.Close();

        }
        static public void PushDataAccount(string naam, string email, string wachtwoord, string achternaam)
        {

            DataTable dataStore = new DataTable();

            SqlConnection connection = new SqlConnection("Data Source=145.24.222.151,8080;" +
                                                         "Database=E-medic;Persist Security Info=True;" +
                                                         "User ID=sa; Password=Teemo1999");





            SqlCommand cmd = new SqlCommand("Insert into Gebruiker(naam,email,wachtwoord,achternaam)values(@naam,@email,@wachtwoord,@achternaam)", connection);
            Console.Write("Entering the Id's.....");
            cmd.Parameters.Add("@naam", SqlDbType.Char, 30).Value = naam;
            cmd.Parameters.Add("@email", SqlDbType.VarChar, 45).Value = email;
            cmd.Parameters.Add("@wachtwoord", SqlDbType.VarChar, 200).Value = wachtwoord;
            cmd.Parameters.Add("@achternaam", SqlDbType.Char, 45).Value = achternaam;


            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                Console.WriteLine("Record Inserted Successfully");
            }
            else
            {
                Console.WriteLine("Operation Failed,Please Try Again Later");
            }
            connection.Close();
        }
    }
}