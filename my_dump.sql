--
-- PostgreSQL database dump
--

\restrict e7rHPnlACeZtTepfJIH38xyi8DCgNfdyDaCZA6ijQekPphNhi64vTYeZfZHm17O

-- Dumped from database version 14.20 (Homebrew)
-- Dumped by pg_dump version 14.20 (Homebrew)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: Domains; Type: TABLE; Schema: public; Owner: grxxx
--

CREATE TABLE public."Domains" (
    "Id" uuid NOT NULL,
    "Name" text,
    "TDL" text,
    "Price" numeric NOT NULL,
    "IsCreated" timestamp with time zone NOT NULL
);


ALTER TABLE public."Domains" OWNER TO grxxx;

--
-- Name: Orders; Type: TABLE; Schema: public; Owner: grxxx
--

CREATE TABLE public."Orders" (
    "Id" uuid NOT NULL,
    "StartDate" timestamp with time zone NOT NULL,
    "ExpiresDate" timestamp with time zone NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "DomainId" uuid NOT NULL,
    "UserId" uuid NOT NULL
);


ALTER TABLE public."Orders" OWNER TO grxxx;

--
-- Name: Users; Type: TABLE; Schema: public; Owner: grxxx
--

CREATE TABLE public."Users" (
    "Id" uuid NOT NULL,
    "Name" text,
    "Email" text,
    "Password" text,
    "Phone" text,
    "IsCreate" timestamp with time zone NOT NULL,
    "IsActive" boolean NOT NULL,
    "Role" text
);


ALTER TABLE public."Users" OWNER TO grxxx;

--
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: grxxx
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO grxxx;

--
-- Data for Name: Domains; Type: TABLE DATA; Schema: public; Owner: grxxx
--

COPY public."Domains" ("Id", "Name", "TDL", "Price", "IsCreated") FROM stdin;
\.


--
-- Data for Name: Orders; Type: TABLE DATA; Schema: public; Owner: grxxx
--

COPY public."Orders" ("Id", "StartDate", "ExpiresDate", "CreatedAt", "DomainId", "UserId") FROM stdin;
\.


--
-- Data for Name: Users; Type: TABLE DATA; Schema: public; Owner: grxxx
--

COPY public."Users" ("Id", "Name", "Email", "Password", "Phone", "IsCreate", "IsActive", "Role") FROM stdin;
93846382-4742-4fef-8b2f-4beed17e86bd	OUIBiubfsdfwq	testasdwqs@test.com	$2a$11$9LubGoEWCixsq3gYC1pBeuSy1m4KsfwYLoiIg0f4IsfPpF1BjpmAO	+79123456789	2026-01-19 18:11:16.111875-08	t	User
af064bb7-cdb6-408f-85ad-ee146587ff6d	Demo User	demo@test.com	$2a$11$kCp7ebf6L4cEzN2SneC1rOjxnDhr2Gq9UUULHp66Q7bTtMUu03Wpq	+79123456789	2026-01-19 19:21:14.368228-08	t	User
15f5883b-0e82-4d9e-8005-bfd0ad8434e2	DemasdasdUser	demoasdsad@test.com	$2a$11$DknThN9i/1PiEX3kQvvoeOZtA/cRy0ZT0HfWIF5PrMpiKZ5/ejMlS	+79123256789	2026-01-20 11:00:13.3107-08	t	User
217bc407-22f9-4d1f-a818-fc048113620d	Dema2sdasdUser	demoaasdsdsad@test.com	$2a$11$bh1aKaxQj9rO20Nip8YNKuW8RUSvJucVMw59q3eIq5WANk8phs1Ye	+79123286789	2026-01-20 11:00:51.189987-08	t	User
4ccf2d6a-7be0-4ad5-9e1f-8b2aeaa0efb5	SemenManari	test46115@example.com	$2a$11$lFUrKXTGWJTo7dC4eTHT.epJTIzwuP39nb4Cn9rIWsQ/2kfDYiKmy	+79159086211	2026-01-24 22:19:23.844494-08	t	User
4b978706-fdd0-4c0b-a557-974dfee81a7c	SemenManari	test16393@example.com	$2a$11$QaSTTHYEQ5eWEKWo9HBN/OL8wgJbY2IULBWcDym1LMpC1a4xm.FPK	+79159086211	2026-01-24 22:49:50.676666-08	t	User
8926d735-5249-436c-bb03-156bd68bee32	SemenManari	test5103@example.com	$2a$11$BJXIdBiupdlpF.4e3lKgNOsZfrUqsuHiIX6ca.0.pi6Cf9Tx/J0Hi	+79159086211	2026-01-24 22:51:02.582231-08	t	User
01e25486-3b50-45bb-a2fd-a189ea4a1190	SemenManari	test8002@example.com	$2a$11$qYQevERELnT20pDHqtv6QOctbEk.R/nGVJF3JIvo3BzmuFnE90hhW	+79159086211	2026-01-24 22:51:51.16722-08	t	User
40cb6ef4-8b80-4e6d-8295-4e0574c20eb2	SemenManari	test92644@example.com	$2a$11$PS3clkw.VqOkreQuf7XmIOPA/.ApsrdYDRQctJzTG6FK8l9iIv2XC	+79159086211	2026-01-25 15:59:08.002228-08	t	User
a5ddd24f-4565-41e9-aa19-99da16e2b4b1	SemenManari	test96697@example.com	$2a$11$zh218mhGYN3xdTUUxEnIS.fb851vRi5FNxkYj/JLZsVflfhsE0ZZS	+79159086211	2026-01-25 15:59:08.21567-08	t	User
4bb7be34-ad31-43b5-89e9-06d48d737eba	SemenManari	test64880@example.com	$2a$11$.E4zVR/WdFEdtlAOg2wX/eCvkXIaJxyLkWRBqUjHJBCXbTC1tUfL6	+79159086211	2026-01-25 17:14:06.103193-08	t	User
f238dc92-9080-4794-861b-acc9075fa08b	SemenManari	test83458@example.com	$2a$11$JEYoD1RMPd3i/rhzsCPfhOG2nkNCeMKmwdKguqWzMy5cIPDPAwFnO	+79159086211	2026-01-25 17:14:06.30172-08	t	User
40b63416-09ca-4df3-aa86-1725c9e19244	SemenManari	test55848@example.com	$2a$11$7sBx67Bw3U8THr/9mfpcf.ualDqcs8IqveSXdtCdQM8ZQncKIQS2a	+79159086211	2026-01-25 17:15:57.268749-08	t	User
05328202-3b6c-4605-b3bc-ea97cc59b562	SemenManari	test29014@example.com	$2a$11$9PLYWuDpmKFEpEpg6AvTD.QW0FMxulN3zIKruILLrF4fqRm9fy4HS	+79159086211	2026-01-25 17:15:57.467502-08	t	User
73a39d18-e0e0-4a45-b899-d1013014d43d	UGT6Fsdfsdf	te s t39318@ex ample.com	$2a$11$qHudq0K3WQctOclciGqX6uaXnpI5LuVHQlb/KHPDoI75ZI2kaBBhW	+79159086211	2026-01-25 17:20:39.822151-08	t	User
23425d4f-5f4c-4c6e-8119-861b132ef2a8	Lhlushdkf	test29407@example.com	$2a$11$k9VSS9PXoKzqoh30UZuXy.Z2DgIlE028AqyhPmHVrIDn4eehkgBBS	+79159086211	2026-01-25 17:20:39.822432-08	t	User
0dcf9718-6149-46b7-a66b-21c306867ae4	Users22x	test80721@example.com	$2a$11$Q7HnvqsC11kcQusuMvbw5OMI./1kSqlTFtjSkKvHm5YXu3HmaaOZ2	+79159086211	2026-01-25 17:20:40.071252-08	t	User
73c9e110-d75f-4450-8d71-c40d0a250b93	Uesrscslk	test45626@example.com	$2a$11$n4bdHy3VISK0IxqQ7hFwyeMVyAJMn6lkpAgC8uAs4uR7xkAbL4EGi	+79159086211	2026-01-25 17:20:40.071063-08	t	User
ef977260-30ea-4949-ad26-33a700ad9c53	Lhlushdkf	test61858@example.com	$2a$11$wvTcMV7cIv33GZO3CAEsTugfzx5N2Gpv25UutmT95MyqX/C8rqNS6	+79159086211	2026-01-25 17:22:03.239914-08	t	User
799ab408-32b9-4d4c-8cd0-4952ec7ba570	UGT6Fsdfsdf	te s t10566@ex ample.com	$2a$11$lBf4r67Xb3fjeuYzS45JeuUTN5SjCyeadFPFZx4I3h6B0ph480lSC	+79159086211	2026-01-25 17:22:03.239659-08	t	User
6cd9a630-97c4-4b63-8e93-834d62de8d44	Uesrscslk	test93833@example.com	$2a$11$dGJpLsrQDUl86YLqZoMNXOhcDnzPcPtUXpOaHYT8wEnb07gbyt8qm	+79159086211	2026-01-25 17:22:03.486751-08	t	User
adb6bcc7-7a5e-49c0-b94f-64f8072052f7	UGT6Fsdfsdf	test42534           @example.com	$2a$11$ATbVz1qNylrbP9l.q2y4BOJAlZsHiHzpVFV0nSgBgSp4CSROJnf0K	+79159086211	2026-01-25 17:23:22.143985-08	t	User
20a0050e-1e64-49ca-b194-22905c22b83e	Lhlushdkf	test98965@example.com	$2a$11$lQz1RRWsMAsxngeufILmOufmcyoNYK.a187eaNN4Tyzu4P3buNx0S	+79159086211	2026-01-25 17:23:22.144232-08	t	User
3a9d4e40-c60a-4ba3-913c-7202b67b9b37	Uesrscslk	test6384@example.com	$2a$11$BfTzxL.ydW3W5gdukKQvzedl1dbXBw2y.jfnGoGcsJW3SVg3FbLi.	+79159086211	2026-01-25 17:23:22.38544-08	t	User
e24d8ca6-43e8-484d-b2de-75710af897a6	Lhlushdkf	test86186@example.com	$2a$11$ZnMo5Kw30AaV3CWs8HHErOaICbtGSkvu2Y6nwNlhvdZzQ9FiKAwDy	+79159086211	2026-01-25 17:24:27.271132-08	t	User
dee91b3a-3e65-47d9-b3b0-330d362b127e	UGT6Fsdfsdf	test93776@example.com    	$2a$11$NOwe0ZXmH96MTEm7dCfDLul0ZbSbZqDZNz5QcpZfVy69pbG7eC22q	+79159086211	2026-01-25 17:24:27.27215-08	t	User
19e50870-069c-46da-a193-e2973eafd5cc	Uesrscslk	test9297@example.com	$2a$11$/YdMPyqsBs5kc9mEsGIqm.lLA7RdazGmvlb.mD6A6MJzdkfLDtFw.	+79159086211	2026-01-25 17:24:27.514466-08	t	User
c1417f97-b259-4c9a-b88a-6a3619b9e0fd	Lhlushdkf	test92028@example.com	$2a$11$4DuN4txCkO2v7z3jBk.7meNJf7/6bMMpzzrANqe/B.WWxAHe.bJKC	+79159086211	2026-01-25 18:00:53.768907-08	t	User
54512bba-f9a6-4346-93a1-02416ebb4262	Uesrscslk	test58215@example.com	$2a$11$OJeDTIisSqMnxcbMNjQpd.LuI6R7qCoUKwLOeYcyhPgz7dy/vSrOa	+79159086211	2026-01-25 18:00:53.969514-08	t	User
8a3b8f43-712c-40a6-8e8e-dbad631e0f41	Dsdfi7IBd	test53420@example.com	$2a$11$Pbh4vPT4chTJgiXxzmk12e1OeE7Y9VRo2OT1TOel6Jks/o6Mc3NNa	+79159086211	2026-01-25 18:10:13.44572-08	t	User
b2276ed1-374f-43b9-9302-8094933f410b	Lhlushdkf	test88054@example.com	$2a$11$qgdnDmOnyqmKyrt7PWv.GukvlxM4wbjqaPCYShgbcMUmD6ZFQmDyu	+79159086211	2026-01-25 18:10:13.446124-08	t	User
b3bece31-b3e8-4883-90b7-3a4f2e7fcada	Uesrscslk	test53187@example.com	$2a$11$OVnICT86AZxhf7UJEAXTDuTmZrsWVR5UZZa9/XSSGY5.g1TDYj4xG	+79159086211	2026-01-25 18:10:13.680383-08	t	User
1e0a7e0c-dece-4ddd-b260-9edce70b7736	AdminFirst	transactxofficial@gmail.com	$2a$11$UVaSZuXWQ7K/4Mc9lS0ofun/GBQy0KKDRbLPXKBbf5oszDXmyEHX6	+78192748210	2026-01-25 22:52:36.213688-08	t	Admin
\.


--
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: grxxx
--

COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
20260117110236_InitialCreate	10.0.2
20260117110510_SecondCreate	10.0.2
20260120030051_DockerTest	10.0.2
20260124215819_Domain	10.0.2
20260126091004_DockerMigr	10.0.2
\.


--
-- Name: Domains PK_Domains; Type: CONSTRAINT; Schema: public; Owner: grxxx
--

ALTER TABLE ONLY public."Domains"
    ADD CONSTRAINT "PK_Domains" PRIMARY KEY ("Id");


--
-- Name: Orders PK_Orders; Type: CONSTRAINT; Schema: public; Owner: grxxx
--

ALTER TABLE ONLY public."Orders"
    ADD CONSTRAINT "PK_Orders" PRIMARY KEY ("Id");


--
-- Name: Users PK_Users; Type: CONSTRAINT; Schema: public; Owner: grxxx
--

ALTER TABLE ONLY public."Users"
    ADD CONSTRAINT "PK_Users" PRIMARY KEY ("Id");


--
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: grxxx
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- Name: IX_Orders_DomainId; Type: INDEX; Schema: public; Owner: grxxx
--

CREATE INDEX "IX_Orders_DomainId" ON public."Orders" USING btree ("DomainId");


--
-- Name: IX_Orders_UserId; Type: INDEX; Schema: public; Owner: grxxx
--

CREATE INDEX "IX_Orders_UserId" ON public."Orders" USING btree ("UserId");


--
-- Name: Orders FK_Orders_Domains_DomainId; Type: FK CONSTRAINT; Schema: public; Owner: grxxx
--

ALTER TABLE ONLY public."Orders"
    ADD CONSTRAINT "FK_Orders_Domains_DomainId" FOREIGN KEY ("DomainId") REFERENCES public."Domains"("Id") ON DELETE CASCADE;


--
-- Name: Orders FK_Orders_Users_UserId; Type: FK CONSTRAINT; Schema: public; Owner: grxxx
--

ALTER TABLE ONLY public."Orders"
    ADD CONSTRAINT "FK_Orders_Users_UserId" FOREIGN KEY ("UserId") REFERENCES public."Users"("Id") ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--

\unrestrict e7rHPnlACeZtTepfJIH38xyi8DCgNfdyDaCZA6ijQekPphNhi64vTYeZfZHm17O

