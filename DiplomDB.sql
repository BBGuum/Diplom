-- This script was generated by the ERD tool in pgAdmin 4.
-- Please log an issue at https://github.com/pgadmin-org/pgadmin4/issues/new/choose if you find any bugs, including reproduction steps.
BEGIN;


CREATE TABLE IF NOT EXISTS public."Carts"
(
    "UserId" integer NOT NULL,
    "ProductId" integer NOT NULL,
    "Count" integer NOT NULL,
    CONSTRAINT "Carts_UserId_ProductId_key" UNIQUE ("UserId", "ProductId")
);

CREATE TABLE IF NOT EXISTS public."Categories"
(
    "Id" serial NOT NULL,
    "Name" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "Categories_pkey" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS public."OrderProduct"
(
    "OrderId" integer NOT NULL,
    "ProductId" integer NOT NULL,
    "Count" integer NOT NULL,
    CONSTRAINT "OrderProduct_OrderId_ProductId_key" UNIQUE ("OrderId", "ProductId")
);

CREATE TABLE IF NOT EXISTS public."OrderStatuses"
(
    "Id" integer NOT NULL DEFAULT nextval('"OrderStatus_Id_seq"'::regclass),
    "Name" character varying(20) COLLATE pg_catalog."default",
    CONSTRAINT "OrderStatus_pkey" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS public."Orders"
(
    "Id" serial NOT NULL,
    "UserId" integer NOT NULL,
    "StatusId" integer NOT NULL,
    "Name" character varying(40) COLLATE pg_catalog."default" NOT NULL,
    "DateGet" timestamp without time zone NOT NULL,
    CONSTRAINT "Orders_pkey" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS public."Products"
(
    "Id" serial NOT NULL,
    "CategoryId" integer NOT NULL,
    "Name" character varying(100) COLLATE pg_catalog."default" NOT NULL,
    "Price" integer NOT NULL,
    "ImageSource" character varying(500) COLLATE pg_catalog."default" NOT NULL,
    "Definition" character varying(800) COLLATE pg_catalog."default" NOT NULL,
    "Count" integer NOT NULL,
    CONSTRAINT "Products_pkey" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS public."Roles"
(
    "Id" serial NOT NULL,
    "Name" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "Roles_pkey" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS public."Users"
(
    "Id" serial NOT NULL,
    "RoleId" integer NOT NULL,
    "Login" character varying(20) COLLATE pg_catalog."default" NOT NULL,
    "Password" character varying(20) COLLATE pg_catalog."default" NOT NULL,
    "Name" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "Phone" character varying(20) COLLATE pg_catalog."default" NOT NULL,
    "ImageSource" character varying(500) COLLATE pg_catalog."default",
    CONSTRAINT "Users_pkey" PRIMARY KEY ("Id")
);

ALTER TABLE IF EXISTS public."Carts"
    ADD CONSTRAINT "Carts_ProductId_fkey" FOREIGN KEY ("ProductId")
    REFERENCES public."Products" ("Id") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;


ALTER TABLE IF EXISTS public."Carts"
    ADD CONSTRAINT "Carts_UserId_fkey" FOREIGN KEY ("UserId")
    REFERENCES public."Users" ("Id") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;


ALTER TABLE IF EXISTS public."OrderProduct"
    ADD CONSTRAINT "OrderProduct_OrderId_fkey" FOREIGN KEY ("OrderId")
    REFERENCES public."Orders" ("Id") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;


ALTER TABLE IF EXISTS public."OrderProduct"
    ADD CONSTRAINT "OrderProduct_ProductId_fkey" FOREIGN KEY ("ProductId")
    REFERENCES public."Products" ("Id") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;


ALTER TABLE IF EXISTS public."Orders"
    ADD CONSTRAINT "Orders_StatusId_fkey" FOREIGN KEY ("StatusId")
    REFERENCES public."OrderStatuses" ("Id") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;


ALTER TABLE IF EXISTS public."Orders"
    ADD CONSTRAINT "Orders_UserId_fkey" FOREIGN KEY ("UserId")
    REFERENCES public."Users" ("Id") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;


ALTER TABLE IF EXISTS public."Products"
    ADD CONSTRAINT "Products_CategoryId_fkey" FOREIGN KEY ("CategoryId")
    REFERENCES public."Categories" ("Id") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;


ALTER TABLE IF EXISTS public."Users"
    ADD CONSTRAINT "Users_RoleId_fkey" FOREIGN KEY ("RoleId")
    REFERENCES public."Roles" ("Id") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;

END;