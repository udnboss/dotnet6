drop table if exists [login];
create table [login] (
  id text
  , email text not null
  , password text not null
  , primary key (id)
  , unique (email)
);

drop table if exists [user];
create table [user] (
  id text
  , name text not null
  , email text not null
  , login_id text not null
  , primary key (id)
  , foreign key (login_id) references login(id) on delete cascade on delete cascade
);

drop table if exists [role];
create table [role] (
  id text not null
  , name text not null
  , primary key (id)
  , unique (name)
);

drop table if exists [permission];
create table [permission] (
  id text not null
  , name text not null
  , entity text not null
  , action text not null
  , primary key (id)
  , unique (name)
  , check ([action] in ('READ', 'CREATE', 'UPDATE', 'DELETE', 'EXECUTE'))
);

drop table if exists [rolePermission];
create table [rolePermission] (
  id text not null
  , role_id text not null
  , permission_id text not null
  , primary key (id)
  , unique (role_id, permission_id)
  , foreign key (role_id) references role(id) on delete cascade on delete cascade
  , foreign key (permission_id) references permission(id) on delete cascade on delete cascade
);

drop table if exists [account];
create table [account] (
  id text
  , label text not null
  , bank_name text not null
  , bank_address text not null
  , bank_swift text not null
  , account_name text not null
  , account_iban text not null
  , account_address text not null
  , primary key (id)
  , unique (label)
);

drop table if exists [category];
create table [category] (
  id text
  , name text not null
  , category_id text
  , primary key (id)
  , unique (name)
  , foreign key (category_id) references category(id) on delete cascade on delete cascade
);

drop table if exists [company];
create table [company] (
  id text
  , name text not null
  , address text not null
  , crn text not null
  , trn text not null
  , contact text not null
  , mobile text not null
  , email text not null
  , primary key (id)
  , unique (name)
);

drop table if exists [currency];
create table [currency] (
  id text not null
  , name text not null
  , symbol text not null
  , primary key (id)
  , unique (name)
);

drop table if exists [customer];
create table [customer] (
  id text
  , name text not null
  , address text
  , contact text
  , currency_id text
  , payment_term int
  , primary key (id)
  , unique (name)
  , foreign key (currency_id) references currency(id) on delete cascade on delete cascade
  , check ([payment_term] between 0 and 90)
);

drop table if exists [item];
create table [item] (
  id text
  , name text not null
  , category_id text
  , primary key (id)
  , unique (name)
  , foreign key (category_id) references category(id) on delete cascade on delete cascade
);

drop table if exists [sale];
create table [sale] (
  id text
  , company_id text not null
  , account_id text not null
  , customer_id text not null
  , currency_id text not null
  , place text
  , number int IDENTITY(1,1)
  , date text not null
  , total real default 0
  , totalItems int default 0
  , reference text
  , confirmed int not null
  , reference_date text
  , due_date text
  , primary key (id)
  , unique (number)
  , foreign key (company_id) references company(id) on delete cascade on delete cascade
  , foreign key (account_id) references account(id) on delete cascade on delete cascade
  , foreign key (customer_id) references customer(id) on delete cascade on delete cascade
  , foreign key (currency_id) references currency(id) on delete cascade on delete cascade
  , check ([due_date] >= '$date')
);

drop table if exists [saleItem];
create table [saleItem] (
  id text
  , sale_id text not null
  , item_id text not null
  , description text
  , quantity int not null
  , price real not null
  , total real
  , primary key (id)
  , unique (sale_id, item_id)
  , foreign key (sale_id) references sale(id) on delete cascade on delete cascade
  , foreign key (item_id) references item(id) on delete cascade on delete cascade
  , check ([quantity] > 0),
  check ([price] >= 0)
);

/* INDEXES */
create index ix_login_email on login (email);

create index ix_user_name on user (name);
create index ix_user_email on user (email);
create index ix_user_login_id on user (login_id);

create index ix_role_name on role (name);

create index ix_permission_name on permission (name);

create index ix_rolePermission_role_id on rolePermission (role_id);
create index ix_rolePermission_permission_id on rolePermission (permission_id);

create index ix_account_label on account (label);

create index ix_category_name on category (name);
create index ix_category_category_id on category (category_id);

create index ix_company_name on company (name);

create index ix_currency_name on currency (name);

create index ix_customer_name on customer (name);
create index ix_customer_currency_id on customer (currency_id);

create index ix_item_name on item (name);
create index ix_item_category_id on item (category_id);

create index ix_sale_number on sale (number);
create index ix_sale_date on sale (date);
create index ix_sale_company_id on sale (company_id);
create index ix_sale_account_id on sale (account_id);
create index ix_sale_customer_id on sale (customer_id);
create index ix_sale_currency_id on sale (currency_id);

create index ix_saleItem_sale_id on saleItem (sale_id);
create index ix_saleItem_item_id on saleItem (item_id);

/* TRIGGERS */
/* after create or update, set the saleItem.total then set sale.total to the sum */
create trigger tr_saleItem_after_insert_update_total
after insert on [saleItem]
begin
    update [saleItem] set [total] = quantity * price where id = new.id;

    update [sale] set [total] = (select ifnull(sum(total), 0) from [saleItem] where [sale_id] = [sale].[id]), [totalItems] = (select ifnull(count(id), 0) from [saleItem] where [sale_id] = [sale].[id]) where [id] = (select [sale_id] from [saleItem] where id = new.id);
end;

/* after create or update, set the saleItem.total then set sale.total to the sum */
create trigger tr_saleItem_after_update_update_total
after update on [saleItem]
begin
    update [saleItem] set [total] = quantity * price where id = new.id;

    update [sale] set [total] = (select ifnull(sum(total), 0) from [saleItem] where [sale_id] = [sale].[id]), [totalItems] = (select ifnull(count(id), 0) from [saleItem] where [sale_id] = [sale].[id]) where [id] = (select [sale_id] from [saleItem] where id = new.id);
end;