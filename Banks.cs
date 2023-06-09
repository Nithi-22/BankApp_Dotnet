using System;
using System.IO;
using System.Text;
using MySql.Data.MySqlClient;
public  class Bank {
    static string connectionDB = @"server=localhost;port=3306;userid=root;password=nithi22jiji;database=db;";
    static MySqlConnection connection = new MySqlConnection(connectionDB);

    //welcome msg
    public void Welcome(){
       Console.WriteLine("------------------------------------");
        Console.WriteLine("Welcome to Banking app");
        Console.WriteLine("------------------------------------");
        Console.WriteLine("Login or new user ?");

    }
    //Credit transaction takes place :
    public void credit(string pin,int amount){
        int amt=0;
        if(amount==0){
          Console.WriteLine("Enter the amount");
              
              amt=Convert.ToInt32(Console.ReadLine());
        }
        else{
            amt=amount;
        }
             

              using (connection)
            {
                try
                {
                    connection.Open();
               
                     string cmd = "select Balance from CustomerDetails where custpin=@pin ";
                        

                    MySqlCommand command = new MySqlCommand(cmd, connection);
                    command.Parameters.AddWithValue("@pin",pin);
                    MySqlDataReader reader = command.ExecuteReader();
                    if(reader.HasRows){
                        while(reader.Read()){
                     amt=amt+reader.GetInt32(0);
                        
                    }
                    }
                    
                    connection.Close();
                    connection.Open();
                    //updating the existing balance amount with the credited amount
                    string cmd1="update  CustomerDetails set Balance=@amt where custpin=@pin";
                     MySqlCommand command1 = new MySqlCommand(cmd1, connection);
                    command1.Parameters.AddWithValue("@amt",amt);
                    command1.Parameters.AddWithValue("@pin",pin);
                      MySqlDataReader reader1 = command1.ExecuteReader();
                    
                    while (reader1.Read()){
                      
                    }
                    Console.WriteLine("Amount credited Sucessfully");
                }
                    
                    catch (MySqlException e)
                    {
                        Console.WriteLine("Error: " + e.Message.ToString());
                    }
                    
                    connection.Close();}
             
             
                   
              }
              //Debit takes place 
              public void debit(string pin)
    {
        Console.WriteLine("Enter the amount");
        int amt=0;
        amt = Convert.ToInt32(Console.ReadLine());
        int x=0;
    

        using (connection)
        {
            try
            {
                connection.Open();
                   //checking whether the amt that we need to debit is lesser than the account balance amount
                string cmd = "select Balance from CustomerDetails where Balance>=@amt and custpin=@pin ";
                  
                MySqlCommand command = new MySqlCommand(cmd, connection);
                command.Parameters.AddWithValue("@amt", amt);
                command.Parameters.AddWithValue("@pin", pin);
                MySqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows){
                    while(reader.Read()){
                        amt=reader.GetInt32(0)-amt;
                        x=1;

                    }
                    connection.Close();
                    
                }else{
                    System.Console.WriteLine("no row");
                }
                if(x==1){
                    //updating the balance amount after debiting
                    connection.Open();
                    string cmd1 ="update CustomerDetails set Balance=@amt where custpin=@pin";
                    MySqlCommand command1 = new MySqlCommand(cmd1,connection);
                    command1.Parameters.AddWithValue("@amt",amt);
                    command1.Parameters.AddWithValue("@pin",pin);
                    MySqlDataReader reader1 = command1.ExecuteReader();
                    while(reader1.Read()){
                       
                    }
                }else{
                    System.Console.WriteLine("None");
                }
            }

            catch (MySqlException e)
            {
                Console.WriteLine("Error: " + e.Message.ToString());
            }
          Console.WriteLine("Amount debited sucessfully");
            connection.Close();
}

}
             
            //Providing loan
              public void loan(string pin){
                  
                  int choice;
                  int amtForEducationalLoan=150000,amtForPersonalLoan=200000;
                  do{
                    System.Console.WriteLine("What type of loan do you want :1.Educational Loan 2.Personal Loan 3. End");
                    choice=Convert.ToInt32(Console.ReadLine());
                    switch(choice){
                        case 1:
                        credit(pin,amtForEducationalLoan);
                        break;
                        case 2 :
                         credit(pin,amtForPersonalLoan);
                         break;
                         case 3 :
                         break;
                    }
                  }while(choice!=3);




              }
              int accountno;
              long ifsccode;
              public void transfer(string pin){
                Console.WriteLine("1. Add beneficiary\n 2. Quick Payment");
                int option=Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the accountno you want to send money to ");
                 accountno=Convert.ToInt32(Console.ReadLine());
                 Console.WriteLine("Enter the ifsccode please ");
                 ifsccode=Convert.ToInt64(Console.ReadLine());
                Console.WriteLine("Enter the amount");
                 int amt=0;
                 amt = Convert.ToInt32(Console.ReadLine());
                 int money=amt;
                 int x=0,y=0;
                 
                using (connection)
        {
               
                if(option==2){
            try
            {
                connection.Open();
                   //checking whether the amt that we need to debit is lesser than the account balance amount
                string cmd = "SELECT Balance from customerdetails where custpin=@pin     ";
                  
                MySqlCommand command = new MySqlCommand(cmd, connection);
                //command.Parameters.AddWithValue("@accountno", accountno);
                command.Parameters.AddWithValue("@pin", pin);
                //command.Parameters.AddWithValue("@accountno", accountno);
                MySqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows){
                    while(reader.Read()){
                        
                        amt=reader.GetInt32(0)-amt;
                        x=1;

                    }
                    connection.Close();
                    
                }else{
                    System.Console.WriteLine("no row");
                }
                connection.Close();
                if(x==1){
                    //updating the balance amount after debiting
                    connection.Open();
                    string cmd1 ="update CustomerDetails set Balance=@amt where custpin=@pin";
                    MySqlCommand command1 = new MySqlCommand(cmd1,connection);
                    command1.Parameters.AddWithValue("@amt",amt);
                    command1.Parameters.AddWithValue("@pin",pin);

                    //command1.Parameters.AddWithValue("@accountno",accountno);

                    MySqlDataReader reader1 = command1.ExecuteReader();
                    while(reader1.Read()){
                       
                    }
                }else{
                    System.Console.WriteLine("None");
                }

            }

            catch (MySqlException e)
            {
                Console.WriteLine("Error: " + e.Message.ToString());
            }
         
            connection.Close();
             
         
                try
                {
                    connection.Open();
               
                     string cmd3 = "select balanceamt from collegedetails where   accountno=@accountno and ifsccode=@ifsccode";
                        

                    MySqlCommand command3 = new MySqlCommand(cmd3, connection);
                    command3.Parameters.AddWithValue("@accountno",accountno);
                    command3.Parameters.AddWithValue("@ifsccode",ifsccode);
                    //command3.Parameters.AddWithValue("@money",amt);
                    MySqlDataReader reader3 = command3.ExecuteReader();
                    if(reader3.HasRows){
                        while(reader3.Read()){
                        money=reader3.GetInt32(0)+money;
                        y=1;
                        
                    }
                    connection.Close();
                    }
                    
                    connection.Close();
                    if(y==1){
                    connection.Open();
                    //updating the existing balance amount with the credited amount
                    string cmd1="update  collegedetails set balanceamt=@money where accountno=@accountno ";
                     MySqlCommand command1 = new MySqlCommand(cmd1, connection);
                    command1.Parameters.AddWithValue("@money",money);
                    command1.Parameters.AddWithValue("@accountno",accountno);
                    
                      MySqlDataReader reader1 = command1.ExecuteReader();
                    
                    while (reader1.Read()){
                      
                    }
                }
                
                    
                }
                    
                    catch (MySqlException e)
                    {
                        Console.WriteLine("Error: " + e.Message.ToString());
                    }
                    
                    
                    connection.Close();

        }
        else{
             
                try
                {
                    connection.Open();
               
                     string cmd3 = "select Balance from customerdetails where   Accountno=@accountno and ifsccode=@ifsccode ";
                        

                    MySqlCommand command3 = new MySqlCommand(cmd3, connection);
                    command3.Parameters.AddWithValue("@accountno",accountno);
                    command3.Parameters.AddWithValue("@ifsccode",ifsccode);
                    //command3.Parameters.AddWithValue("@money",amt);
                    MySqlDataReader reader3 = command3.ExecuteReader();
                    if(reader3.HasRows){
                        while(reader3.Read()){
                        money=reader3.GetInt32(0)+money;
                        y=1;
                        
                    }
                    connection.Close();
                    }
                    
                    connection.Close();
                    if(y==1){
                    connection.Open();
                    //updating the existing balance amount with the credited amount
                    string cmd1="update  customerdetails set Balance=@money where Accountno=@accountno and ifsccode=@ifsccode";
                     MySqlCommand command1 = new MySqlCommand(cmd1, connection);
                    command1.Parameters.AddWithValue("@money",money);
                    command1.Parameters.AddWithValue("@accountno",accountno);
                    command1.Parameters.AddWithValue("@ifsccode",ifsccode);
                      MySqlDataReader reader1 = command1.ExecuteReader();
                    
                    while (reader1.Read()){
                      
                    }
                }
                
                    
                }
                    
                    catch (MySqlException e)
                    {
                        Console.WriteLine("Error: " + e.Message.ToString());
                    }
                   
                    
                    connection.Close();


                    

                    

                
        
                }
                   
        } 
      Console.WriteLine("Amount transferred successfully");
       }
       
}