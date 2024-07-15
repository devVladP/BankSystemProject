"# BankSystemProject" 
# BankSystemProject
Web Api and back-end part of my project Bank System. This project was developed with using Onion architecture (layers architecture) and pattern MediatR.
Authentication and Authorization were provided using JWT tokens and service Auth0. Application already have 2 test users, but you can add new using this path: "https://localhost:**_your_port_**/api/auth/login". This path will lead you to Auth sign in page, where you can register new user.
Test users:
Client: Login: clienttom@example.bank.project; Password: P@ssw0rd_1;
Employee: Login: employeedan@example.bank.project; Password: P@ssw0rd_1;

Clients can add cards, transfer money to the other cards and take credits, also clients can update their information and pay credits.
Employees can view all clients, cards and credits in database.
