# s3318814-a2

#Records
The records feature has been used in the following models: Login, Customer, Transaction, Account, Payee, BillPay.
In the LoginController, the PasswordChange method has an example of mutating the record.

As a record is intended to be used with read-only data, it offers performance gains as the same memory address is simply passed around (pass by reference). Its immutability means that it reduces the chance of data being accidentally changed which could produce bugs. Although primarily used for immutable objects, records do allow for mutation through creating a new instance. It simplifies the process as it automatically copies properties from the old instance to the new one and updates the properties being changed. This forces the programmer to be deliberate about any changes made and means they only need write code for the properties being changed. Its use with the models reflects that care should be taken in manipulating the data so as to avoid unnecessary mistakes and changes being written back to the database.



Target something that you know doesn't change once create or rarely changes once created.

Method can't corrupt the data as it is immutable.

A b = a with { Name = Bolger }; new instance of A as b. All data copied with the new Name. 
 

#PART 4 - Charts (Task L)
This task has been attempted.
The charts can be found within the customer site. After logging in, a "Charts" option is available in the navbar. Clicking on "Charts" will direct the user to the charts page. All charts automatically generate without requiring user intervention. 

#References

https://www.techiedelight.com/join-two-lists-csharp/ - Joining lists
Week records tote
Week paging cute
Week background services




https://www.codegrepper.com/code-examples/csharp/get+list+length+c%23
https://www.chartjs.org/docs/master/charts/mixed.html
https://stackoverflow.com/questions/17884106/how-do-i-force-razor-to-switch-back-to-client-side-code/30098736
https://www.reddit.com/r/webdev/comments/4gjuvp/chartjs_canvas_height_width_ignored/
https://www.chartjs.org/docs/latest/general/data-structures.html
https://stackoverflow.com/questions/37577892/chart-js-ignoring-canvas-height-width
https://www.chartjs.org/docs/latest/charts/bar.html
https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli
https://stackoverflow.com/questions/2146296/adding-a-time-to-a-datetime-in-c-sharp
https://stackoverflow.com/questions/1257482/redirecttoaction-with-parameter
https://docs.microsoft.com/en-us/ef/core/modeling/entity-types?tabs=data-annotations

https://github.com/troygoode/PagedList/blob/master/README.markdown#example - fix for page number
