insert into [login] ([id], [email], [password]) values ('550e8400-e29b-41d4-a716-446655440000', 'john@example.com', 'f515c100dc23c786bcc6ff791fb1c813b38ddcacd0f9546f1e1db0705f4f56e1');
insert into [login] ([id], [email], [password]) values ('550e8400-e29b-41d4-a716-446655440001', 'jane@example.com', 'f515c100dc23c786bcc6ff791fb1c813b38ddcacd0f9546f1e1db0705f4f56e1');
insert into [login] ([id], [email], [password]) values ('550e8400-e29b-41d4-a716-446655440002', 'mike@example.com', 'f515c100dc23c786bcc6ff791fb1c813b38ddcacd0f9546f1e1db0705f4f56e1');
insert into [login] ([id], [email], [password]) values ('550e8400-e29b-41d4-a716-446655440003', 'amy@example.com', 'f515c100dc23c786bcc6ff791fb1c813b38ddcacd0f9546f1e1db0705f4f56e1');

insert into [user] ([id], [name], [email], [login_id]) values ('550e8400-e29b-41d4-a716-446655440000', 'John', 'john@example.com', '550e8400-e29b-41d4-a716-446655440000');
insert into [user] ([id], [name], [email], [login_id]) values ('550e8400-e29b-41d4-a716-446655440001', 'Jane', 'jane@example.com', '550e8400-e29b-41d4-a716-446655440001');
insert into [user] ([id], [name], [email], [login_id]) values ('550e8400-e29b-41d4-a716-446655440002', 'Mike', 'mike@example.com', '550e8400-e29b-41d4-a716-446655440002');
insert into [user] ([id], [name], [email], [login_id]) values ('550e8400-e29b-41d4-a716-446655440003', 'Amy', 'amy@example.com', '550e8400-e29b-41d4-a716-446655440003');

insert into [role] ([id], [code], [name]) values ('550e8400-e29b-41d4-a716-446655440000', 'VIEW', 'Viewer');
insert into [role] ([id], [code], [name]) values ('550e8400-e29b-41d4-a716-446655440001', 'CONTRIBUTE', 'Contributor');
insert into [role] ([id], [code], [name]) values ('550e8400-e29b-41d4-a716-446655440002', 'ADMIN', 'Administrator');

insert into [account] ([id], [label], [bank_name], [bank_address], [bank_swift], [account_name], [account_iban], [account_address]) values ('550e8400-e29b-41d4-a716-446655440000', 'fransi', 'Bank of America', 'Riyad Bank, King Abdulaziz Road, Al Adama, Eastern Province 31952', 'RIBLSARI', 'Ahmed Ali', 'SA44 2000 0001 2345 6789 1234', 'P.O. Box 22622 Riyadh 11416 Saudi Arabia');
insert into [account] ([id], [label], [bank_name], [bank_address], [bank_swift], [account_name], [account_iban], [account_address]) values ('550e8400-e29b-41d4-a716-446655440001', 'ncb', 'Chase', 'Banque Saudi Fransi, Prince Faisal Bin Fahd Road, Al Khobar, Eastern Province 34429', 'BSFRSARI', 'Fatima Hassan', 'SA03 5500 0000 5678 9012 3456', 'P.O. Box 56006 Riyadh 11554 Saudi Arabia');
insert into [account] ([id], [label], [bank_name], [bank_address], [bank_swift], [account_name], [account_iban], [account_address]) values ('550e8400-e29b-41d4-a716-446655440002', 'sab', 'Wells Fargo', 'Al Rajhi Bank, King Fahd Road, Riyadh, Riyadh Province 11432', 'RJHISARI', 'Omar Khan', 'SA32 8000 0000 6080 1016 7519', 'P.O. Box 28 Riyadh 11411 Saudi Arabia');
insert into [account] ([id], [label], [bank_name], [bank_address], [bank_swift], [account_name], [account_iban], [account_address]) values ('550e8400-e29b-41d4-a716-446655440003', 'albilad', 'Citibank', 'Bank AlJazira, King Abdullah Road, Jeddah, Makkah Province 23441', 'BJAZSAJE', 'Sara Mohammed', 'SA29 6000 0000 0068 1000 0014', 'P.O. Box 6277 Jeddah 21442 Saudi Arabia');

insert into [category] ([id], [name]) values ('550e8400-e29b-41d4-a716-446655440000', 'books');
insert into [category] ([id], [name]) values ('550e8400-e29b-41d4-a716-446655440001', 'electronics');
insert into [category] ([id], [name]) values ('550e8400-e29b-41d4-a716-446655440002', 'clothing');
insert into [category] ([id], [name]) values ('550e8400-e29b-41d4-a716-446655440003', 'toys');

insert into [company] ([id], [name], [address], [crn], [trn], [contact], [mobile], [email]) values ('550e8400-e29b-41d4-a716-446655440000', 'Microsoft', 'One Microsoft Way, Redmond, WA 98052', '600413485', '12-3456789', '(425) 882-8080', '+1 (425) 882-8080', 'info@microsoft.com');
insert into [company] ([id], [name], [address], [crn], [trn], [contact], [mobile], [email]) values ('550e8400-e29b-41d4-a716-446655440001', 'Google', '1600 Amphitheatre Parkway, Mountain View, CA 94043', '358269703', '94-3203695', '(650) 253-0000', '+1 (650) 253-0000', 'press@google.com');
insert into [company] ([id], [name], [address], [crn], [trn], [contact], [mobile], [email]) values ('550e8400-e29b-41d4-a716-446655440002', 'Apple', '1 Apple Park Way, Cupertino, CA 95014', '942404361', '91-1144442', '(408) 996-1010', '+1 (408) 996-1010', 'media.help@apple.com');
insert into [company] ([id], [name], [address], [crn], [trn], [contact], [mobile], [email]) values ('550e8400-e29b-41d4-a716-446655440003', 'Amazon', '410 Terry Ave N, Seattle, WA 98109', '916284817', '91-1643330', '(206) 266-1000', '+1 (206) 266-1000', 'ecr-replies@amazon.com');

insert into [currency] ([id], [name], [symbol]) values ('550e8400-e29b-41d4-a716-446655440000', 'United States Dollar', '$');
insert into [currency] ([id], [name], [symbol]) values ('550e8400-e29b-41d4-a716-446655440001', 'Euro', '€');
insert into [currency] ([id], [name], [symbol]) values ('550e8400-e29b-41d4-a716-446655440002', 'Japanese Yen', '¥');
insert into [currency] ([id], [name], [symbol]) values ('550e8400-e29b-41d4-a716-446655440003', 'British Pound', '£');

insert into [customer] ([id], [name], [address], [contact], [currency_id], [payment_term]) values ('550e8400-e29b-41d4-a716-446655440000', 'John Doe', '123 Main St, Anytown USA', '+1 (555) 555-5555', '550e8400-e29b-41d4-a716-446655440000', 30);
insert into [customer] ([id], [name], [address], [contact], [currency_id], [payment_term]) values ('550e8400-e29b-41d4-a716-446655440001', 'Jane Smith', '456 Elm St, Anytown USA', '+1 (555) 555-5556', '550e8400-e29b-41d4-a716-446655440001', 60);
insert into [customer] ([id], [name], [address], [contact], [currency_id], [payment_term]) values ('550e8400-e29b-41d4-a716-446655440002', 'Bob Johnson', '789 Oak St, Anytown USA', '+1 (555) 555-5557', '550e8400-e29b-41d4-a716-446655440002', 90);
insert into [customer] ([id], [name], [address], [contact], [currency_id], [payment_term]) values ('550e8400-e29b-41d4-a716-446655440003', 'Alice Williams', '321 Pine St, Anytown USA', '+1 (555) 555-5558', '550e8400-e29b-41d4-a716-446655440003', 45);

insert into [item] ([id], [name], [category_id]) values ('550e8400-e29b-41d4-a716-446655440000', 'Microsoft Office', '550e8400-e29b-41d4-a716-446655440000');
insert into [item] ([id], [name], [category_id]) values ('550e8400-e29b-41d4-a716-446655440001', 'Adobe Creative Cloud', '550e8400-e29b-41d4-a716-446655440001');
insert into [item] ([id], [name], [category_id]) values ('550e8400-e29b-41d4-a716-446655440002', 'Salesforce CRM', '550e8400-e29b-41d4-a716-446655440002');
insert into [item] ([id], [name], [category_id]) values ('550e8400-e29b-41d4-a716-446655440003', 'Slack', '550e8400-e29b-41d4-a716-446655440003');

insert into [sale] ([id], [account_id], [company_id], [customer_id], [place], [number], [date], [currency_id], [total], [due_date], [reference_date], [confirmed]) values ('550e8400-e29b-41d4-a716-446655440000', '550e8400-e29b-41d4-a716-446655440000', '550e8400-e29b-41d4-a716-446655440000', '550e8400-e29b-41d4-a716-446655440000', 'New York, NY', 1, '2023-01-01', '550e8400-e29b-41d4-a716-446655440000', 0, '2023-02-01', '2023-01-01', 1);
insert into [sale] ([id], [account_id], [company_id], [customer_id], [place], [number], [date], [currency_id], [total], [due_date], [reference_date], [confirmed]) values ('550e8400-e29b-41d4-a716-446655440001', '550e8400-e29b-41d4-a716-446655440001', '550e8400-e29b-41d4-a716-446655440001', '550e8400-e29b-41d4-a716-446655440001', 'Los Angeles, CA', 2, '2023-02-01', '550e8400-e29b-41d4-a716-446655440001', 0, '2023-03-01', '2023-02-01', 1);
insert into [sale] ([id], [account_id], [company_id], [customer_id], [place], [number], [date], [currency_id], [total], [due_date], [reference_date], [confirmed]) values ('550e8400-e29b-41d4-a716-446655440002', '550e8400-e29b-41d4-a716-446655440002', '550e8400-e29b-41d4-a716-446655440002', '550e8400-e29b-41d4-a716-446655440002', 'Chicago, IL', 3, '2023-03-01', '550e8400-e29b-41d4-a716-446655440002', 0, '2023-04-01', '2023-03-01', 0);
insert into [sale] ([id], [account_id], [company_id], [customer_id], [place], [number], [date], [currency_id], [total], [due_date], [reference_date], [confirmed]) values ('550e8400-e29b-41d4-a716-446655440003', '550e8400-e29b-41d4-a716-446655440003', '550e8400-e29b-41d4-a716-446655440003', '550e8400-e29b-41d4-a716-446655440003', 'Houston, TX', 4, '2023-04-01', '550e8400-e29b-41d4-a716-446655440003', 0, '2023-05-01', '2023-04-01', 0);

insert into [saleItem] ([id], [sale_id], [item_id], [quantity], [price]) values ('550e8400-e29b-41d4-a716-446655440000', '550e8400-e29b-41d4-a716-446655440000', '550e8400-e29b-41d4-a716-446655440000', 1, 15.99);
insert into [saleItem] ([id], [sale_id], [item_id], [quantity], [price]) values ('550e8400-e29b-41d4-a716-446655440001', '550e8400-e29b-41d4-a716-446655440000', '550e8400-e29b-41d4-a716-446655440001', 2, 534.95);
insert into [saleItem] ([id], [sale_id], [item_id], [quantity], [price]) values ('550e8400-e29b-41d4-a716-446655440002', '550e8400-e29b-41d4-a716-446655440002', '550e8400-e29b-41d4-a716-446655440002', 3, 42.0);
insert into [saleItem] ([id], [sale_id], [item_id], [quantity], [price]) values ('550e8400-e29b-41d4-a716-446655440003', '550e8400-e29b-41d4-a716-446655440003', '550e8400-e29b-41d4-a716-446655440003', 2, 67.8);


insert into [permission] select uuid4(), 'ENTITY:ACCOUNT:READ', 'Entity account READ', 'account', 'READ';
insert into [permission] select uuid4(), 'ENTITY:ACCOUNT:CREATE', 'Entity account CREATE', 'account', 'CREATE';
insert into [permission] select uuid4(), 'ENTITY:ACCOUNT:UPDATE', 'Entity account UPDATE', 'account', 'UPDATE';
insert into [permission] select uuid4(), 'ENTITY:ACCOUNT:DELETE', 'Entity account DELETE', 'account', 'DELETE';
insert into [permission] select uuid4(), 'ENTITY:ACCOUNT:EXECUTE', 'Entity account EXECUTE', 'account', 'EXECUTE';
insert into [permission] select uuid4(), 'ENTITY:CATEGORY:READ', 'Entity category READ', 'category', 'READ';
insert into [permission] select uuid4(), 'ENTITY:CATEGORY:CREATE', 'Entity category CREATE', 'category', 'CREATE';
insert into [permission] select uuid4(), 'ENTITY:CATEGORY:UPDATE', 'Entity category UPDATE', 'category', 'UPDATE';
insert into [permission] select uuid4(), 'ENTITY:CATEGORY:DELETE', 'Entity category DELETE', 'category', 'DELETE';
insert into [permission] select uuid4(), 'ENTITY:CATEGORY:EXECUTE', 'Entity category EXECUTE', 'category', 'EXECUTE';
insert into [permission] select uuid4(), 'ENTITY:COMPANY:READ', 'Entity company READ', 'company', 'READ';
insert into [permission] select uuid4(), 'ENTITY:COMPANY:CREATE', 'Entity company CREATE', 'company', 'CREATE';
insert into [permission] select uuid4(), 'ENTITY:COMPANY:UPDATE', 'Entity company UPDATE', 'company', 'UPDATE';
insert into [permission] select uuid4(), 'ENTITY:COMPANY:DELETE', 'Entity company DELETE', 'company', 'DELETE';
insert into [permission] select uuid4(), 'ENTITY:COMPANY:EXECUTE', 'Entity company EXECUTE', 'company', 'EXECUTE';
insert into [permission] select uuid4(), 'ENTITY:CURRENCY:READ', 'Entity currency READ', 'currency', 'READ';
insert into [permission] select uuid4(), 'ENTITY:CURRENCY:CREATE', 'Entity currency CREATE', 'currency', 'CREATE';
insert into [permission] select uuid4(), 'ENTITY:CURRENCY:UPDATE', 'Entity currency UPDATE', 'currency', 'UPDATE';
insert into [permission] select uuid4(), 'ENTITY:CURRENCY:DELETE', 'Entity currency DELETE', 'currency', 'DELETE';
insert into [permission] select uuid4(), 'ENTITY:CURRENCY:EXECUTE', 'Entity currency EXECUTE', 'currency', 'EXECUTE';
insert into [permission] select uuid4(), 'ENTITY:CUSTOMER:READ', 'Entity customer READ', 'customer', 'READ';
insert into [permission] select uuid4(), 'ENTITY:CUSTOMER:CREATE', 'Entity customer CREATE', 'customer', 'CREATE';
insert into [permission] select uuid4(), 'ENTITY:CUSTOMER:UPDATE', 'Entity customer UPDATE', 'customer', 'UPDATE';
insert into [permission] select uuid4(), 'ENTITY:CUSTOMER:DELETE', 'Entity customer DELETE', 'customer', 'DELETE';
insert into [permission] select uuid4(), 'ENTITY:CUSTOMER:EXECUTE', 'Entity customer EXECUTE', 'customer', 'EXECUTE';
insert into [permission] select uuid4(), 'ENTITY:ITEM:READ', 'Entity item READ', 'item', 'READ';
insert into [permission] select uuid4(), 'ENTITY:ITEM:CREATE', 'Entity item CREATE', 'item', 'CREATE';
insert into [permission] select uuid4(), 'ENTITY:ITEM:UPDATE', 'Entity item UPDATE', 'item', 'UPDATE';
insert into [permission] select uuid4(), 'ENTITY:ITEM:DELETE', 'Entity item DELETE', 'item', 'DELETE';
insert into [permission] select uuid4(), 'ENTITY:ITEM:EXECUTE', 'Entity item EXECUTE', 'item', 'EXECUTE';
insert into [permission] select uuid4(), 'ENTITY:SALE:READ', 'Entity sale READ', 'sale', 'READ';
insert into [permission] select uuid4(), 'ENTITY:SALE:CREATE', 'Entity sale CREATE', 'sale', 'CREATE';
insert into [permission] select uuid4(), 'ENTITY:SALE:UPDATE', 'Entity sale UPDATE', 'sale', 'UPDATE';
insert into [permission] select uuid4(), 'ENTITY:SALE:DELETE', 'Entity sale DELETE', 'sale', 'DELETE';
insert into [permission] select uuid4(), 'ENTITY:SALE:EXECUTE', 'Entity sale EXECUTE', 'sale', 'EXECUTE';
insert into [permission] select uuid4(), 'ENTITY:SALEITEM:READ', 'Entity saleItem READ', 'saleItem', 'READ';
insert into [permission] select uuid4(), 'ENTITY:SALEITEM:CREATE', 'Entity saleItem CREATE', 'saleItem', 'CREATE';
insert into [permission] select uuid4(), 'ENTITY:SALEITEM:UPDATE', 'Entity saleItem UPDATE', 'saleItem', 'UPDATE';
insert into [permission] select uuid4(), 'ENTITY:SALEITEM:DELETE', 'Entity saleItem DELETE', 'saleItem', 'DELETE';
insert into [permission] select uuid4(), 'ENTITY:SALEITEM:EXECUTE', 'Entity saleItem EXECUTE', 'saleItem', 'EXECUTE';
insert into [rolePermission] select uuid4(), r.id, p.id from role r, permission p;