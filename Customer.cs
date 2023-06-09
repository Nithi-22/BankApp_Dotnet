using System;
using System.Threading;
using MySql.Data.MySqlClient;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.ComponentModel;


using System.Text;
using System.Threading.Tasks;
//using System.Windows.Form;


public class Customer:Bank
{
   //ComboBox mybox = new ComboBox();
          //Label l = new Label();
          public static string EnteredVal = ""; 
    public int pincode,id;
    public string? pin;
    public string? username;
    public string? street,city,state,email="0";
    public string? phoneNo;
    public string? aadharNo;
    public long accountNo;
    public long balance;
    static string connectionDB = @"server=localhost;port=3306;userid=root;password=nithi22jiji;database=db;";
    static MySqlConnection connection = new MySqlConnection(connectionDB);
    public string login()
    {
         int flag=5;
        while (flag>0)
        {
            Console.WriteLine("Enter 4 digit pin :");
            string pin="Please enter the pin";//pin is entered
            //pin = (Console.ReadLine());
            string checkpwd=CheckPassword(pin);
            //Console.ReadKey();
            using (connection)
            {
                try
                {
                    connection.Open();
            //Now,checking whether the pin that is entered is equal to the pin that is present in the database
                    string cmd = "select * from CustomerDetails where custpin=@checkpwd";
                    MySqlCommand command = new MySqlCommand(cmd, connection);
                    command.Parameters.AddWithValue("@checkpwd", checkpwd);
                    MySqlDataReader reader = command.ExecuteReader();

                
                    if (reader.HasRows)
                    {
                        Console.WriteLine("Login Successfull ");
                        transaction(checkpwd);
                        return checkpwd;

                    }
                    else
                    {
                        Console.WriteLine("Login failed !try again !");
                       // Console.WriteLine("Connection is :" + connection.State.ToString() + Environment.NewLine);
                    }
                    
                    connection.Close();

                }
                
                catch (MySqlException e)
                {
                    Console.WriteLine("Error: " + e.Message.ToString());

                }
            }

         flag--;
         System.Console.WriteLine("You have still more "+flag+" chances left");
        }
        if(flag==0){
        Console.WriteLine("Chance is over,you have crossed 5 times");
        Console.WriteLine("Kindly waitt for 60 secs");
        System.Threading.Thread.Sleep(60000);
        login();

        
        }

        
       return "0";
       
    }
    //creation of new account if he/she doesn't have account
    public void createAccount(){
          bool isValidEmail,isValidPhoneNo,isValidAadharNo,isValidPinNo;
          
          Random random =new Random();
          int emailvalue=0;
          int phonevalue=0;
         int aadharvalue=0;
         int pinvalue=0;
         string convertaadharno="0",convertphoneno="0",convertpinno="0";
          System.Console.WriteLine("Enter username");
          username=Console.ReadLine();
          Console.WriteLine("Enter 4 digit pin :");
            string pin="Please enter the pin";//pin is entered
            //pin = (Console.ReadLine());
            while( pinvalue==0){
             convertpinno=CheckPassword(pin);
            Regex regex = new Regex(@"^[0-9]{4}$");
            isValidPinNo=regex.IsMatch(convertpinno);
             if(!isValidPinNo ){
              
              
               Console.WriteLine($"This pin no is invalid");
               Console.WriteLine("Please enter it properly");
               pin="Enter pin no :";
               pinvalue=0;
             
                
               
            } else {
               Console.WriteLine($"This pin no is valid");
               pinvalue=1;
               
            }}
            
           System.Console.WriteLine("Enter street name");
          street=Console.ReadLine();
          System.Console.WriteLine("Enter city");
          city=Console.ReadLine();
          System.Console.WriteLine("Enter state");
          state=Console.ReadLine();
          System.Console.WriteLine("Enter pincode");
          pincode=Convert.ToInt32(Console.ReadLine());
          System.Console.WriteLine("Enter 12 digit aadharNo");
          string aadharNo="Please enter the aadhar No";//pin is entered
           
          
          while( aadharvalue==0){
             convertaadharno=CheckPassword(aadharNo);
            Regex regex = new Regex(@"^[0-9]{12}$");
            isValidAadharNo=regex.IsMatch(convertaadharno);
             if(!isValidAadharNo ){
              
              
               Console.WriteLine($"This Aadhar no is invalid");
               Console.WriteLine("Please enter it properly");
               aadharNo="Enter aadharno :";
               aadharvalue=0;
             
                
               
            } else {
               Console.WriteLine($"This AadharNo is valid");
               aadharvalue=1;
               
            }}
          Console.WriteLine("Enter 10 digit phoneNo");
           phoneNo= "Enter phone no ";
           
          while( phonevalue==0){
            convertphoneno=CheckPassword(phoneNo);
            Regex regex = new Regex(@"^[0-9]{10}$");
            isValidPhoneNo=regex.IsMatch(convertphoneno);
             if(!isValidPhoneNo  ){
              
              
               Console.WriteLine($"This phone No is invalid");
               Console.WriteLine("Please enter it properly");
               phoneNo="Enter phone no";
               phonevalue=0;
             
                
               
            } else {
               Console.WriteLine($"This PhoneNo is valid");
               phonevalue=1;
               
            }}
            
            Console.WriteLine("Enter correct  email");
              email=Console.ReadLine();
             while(emailvalue==0){
              
            Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",RegexOptions.CultureInvariant | RegexOptions.Singleline);
            
               isValidEmail = regex.IsMatch(email);
             
            if(!isValidEmail ){
              
              
               Console.WriteLine($"This email is invalid");
               Console.WriteLine("Please enter it properly");
               email=Console.ReadLine();
               emailvalue=0;
             
                
               
            } else {
               Console.WriteLine($"This email is valid");
               
               emailvalue=1;
               break;
            }}
            
            
           
           
          
         
         
           accountNo=random.Next(9999,999999);
          id=random.Next(10,99);
        
           using (connection ){
            
                try
                {
                  
                    connection.Open();
                     //entering all the details that is needed
                    string cmd = "insert into CustomerDetails(CustName,custId,custpin,street,city,state,pincode,emailid,phoneNo,Accountno,aadharNo,balance) values(@custname,@custid,@convertedpinno,@street,@city,@state,@pincode,@email,@convertedphoneno,@AccountNo,@convertedaadharno,@balance)";
                    MySqlCommand command = new MySqlCommand(cmd, connection);
                    
                    command.Parameters.AddWithValue("@custname", username);
                    command.Parameters.AddWithValue("@custid", id);
                    command.Parameters.AddWithValue("@convertedpinno", convertpinno);
                    command.Parameters.AddWithValue("@street", street);
                    command.Parameters.AddWithValue("@city", city);
                    command.Parameters.AddWithValue("@state", state);
                    command.Parameters.AddWithValue("@pincode", pincode);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@convertedphoneno", convertphoneno);
                    command.Parameters.AddWithValue("@AccountNo", accountNo);
                    command.Parameters.AddWithValue("@convertedaadharno", convertaadharno);
                    command.Parameters.AddWithValue("@balance", balance);
                    MySqlDataReader reader = command.ExecuteReader();
                      while(reader.Read()){}
                     System.Console.WriteLine("Account created successfully..");
                    
                    connection.Close();

                }
                catch (MySqlException e)
                {
                    Console.WriteLine("Error: " + e.Message.ToString());

                }
                
                

           }
          
           //login again after the completion of creating account
           login();
           

    }
    static string CheckPassword(string EnterText)  
        {  
            try  
            {  
                 
                
                //Console.Write(EnterText); 
                EnteredVal = "";  
                
                do  
                {  
                    
                    ConsoleKeyInfo key = Console.ReadKey(true);  
                    
                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)  
                    {  
                        EnteredVal += key.KeyChar;  
                        Console.Write("*"); 
                        
                    }  
                    else  
                    {  
                      // Backspace Should Not Work  
                        if (key.Key == ConsoleKey.Backspace && EnteredVal.Length > 0)  
                        {  
                            EnteredVal = EnteredVal.Substring(0, (EnteredVal.Length - 1));  
                            Console.Write("\b \b");  
                        }  
                        else if (key.Key == ConsoleKey.Enter)  
                        {  
                            if (string.IsNullOrWhiteSpace(EnteredVal))  
                            {  
                                Console.WriteLine("");  
                                Console.WriteLine("Empty value not allowed.");  
                                CheckPassword(EnterText);  
                                break;  
                            } 
                            else  
                            {  
                                Console.WriteLine("");  
                                break;  
                            }  
                        }  
                    }  
                } while (true);  
            }  
            catch (Exception ex )  
            {  
                throw ex;  
            }  
            return EnteredVal;
        }  

    public void transaction(string pin){
        int option;
        do{

              Console.WriteLine("Do you wish to 1.credit/2.debit/3.loan/4.transfer/5.end");
             option=Convert.ToInt32(Console.ReadLine());
            switch(option){
              case 1:{
                credit(pin,0);
                break;
              }
              case 2:{
                debit(pin);
                break;
              }
              case 3:{
                loan(pin);
                break;

              }
              case 4:{
                transfer(pin);
                break;
              }
              case 5:{
                System.Console.WriteLine("Your process has been ended");
                break;
              }
              
            
            }
            }while(option!=5);
            
    }
}