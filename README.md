# mcba

Gerard Anderson
https://github.com/gmanderson/mcba

# LOGINS
Login ID				Password  
12345678 (Matthew Bolger) 	abc123  
38074569 (Rodney Cocker) 		ilovermit2020  
17963428 (Shekhar Kalra) 		youWill_n0tGuess-This!  

For admin site:  
Username: admin Password: admin


# PART 1 - ALL TASKS ATTEMPTED

# Records
The records feature has been used in the following models: Login, Customer, Transaction, Account, Payee, BillPay.
In the LoginController, the PasswordChange method has an example of mutating the record.

As a record is intended to be used with read-only data, it offers performance gains as the same memory address is simply passed around (pass by reference). Its immutability means that it reduces the chance of data being accidentally changed which could produce bugs. Although primarily used for immutable objects, records do allow for mutation through creating a new instance. It simplifies the process as it automatically copies properties from the old instance to the new one and updates the properties being changed. This forces the programmer to be deliberate about any changes made and means they only need write code for the properties being changed. Its use with the models reflects that care should be taken in manipulating the data so as to avoid unnecessary mistakes and changes being written back to the database.

# PART 2 - ALL TASKS ATTEMPTED

# PART 3 - ALL TASKS ATTEMPTED

# PART 4 - Charts (Task L)
This task has been attempted.

The charts can be found within the customer site. After logging in, a "Charts" option is available in the navbar. Clicking on "Charts" will direct the user to the charts page. All charts automatically generate without requiring user intervention. 

# References
Code has been based on CPT373 weekly tutorial and webinar code (weeks 4 - 10)

Microsoft, 2021, .NET Documentation, <https://docs.microsoft.com/en-au/dotnet/fundamentals/>

Microsoft, 2021, C# Documentation, <https://docs.microsoft.com/en-au/dotnet/csharp/>

Microsoft, 2021, Entity Framework Documentation, <https://docs.microsoft.com/en-us/ef/>

Chart.js, 2021, Chart.js, <https://www.chartjs.org>
