--
-- PostgreSQL database dump
--

-- Dumped from database version 17.5
-- Dumped by pg_dump version 17.5

-- Started on 2025-06-30 23:25:32

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
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
-- TOC entry 222 (class 1259 OID 16411)
-- Name: bookings; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.bookings (
    id integer NOT NULL,
    courier_id character varying(50) NOT NULL,
    motorcycle_id character varying(50) NOT NULL,
    start_date timestamp without time zone NOT NULL,
    end_date timestamp without time zone NOT NULL,
    expected_end_date timestamp without time zone NOT NULL,
    plan integer NOT NULL
);


ALTER TABLE public.bookings OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 16389)
-- Name: couriers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.couriers (
    id integer NOT NULL,
    identifier character varying(50) NOT NULL,
    name character varying(100) NOT NULL,
    tax_id character(14) NOT NULL,
    birth_date date NOT NULL,
    license_number character varying(20) NOT NULL,
    license_type character(2) NOT NULL,
    license_image text
);


ALTER TABLE public.couriers OWNER TO postgres;

--
-- TOC entry 217 (class 1259 OID 16388)
-- Name: couriers_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.couriers_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.couriers_id_seq OWNER TO postgres;

--
-- TOC entry 4931 (class 0 OID 0)
-- Dependencies: 217
-- Name: couriers_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.couriers_id_seq OWNED BY public.couriers.id;


--
-- TOC entry 220 (class 1259 OID 16400)
-- Name: motorcycles; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.motorcycles (
    id integer NOT NULL,
    identifier character varying(50) NOT NULL,
    year integer NOT NULL,
    model character varying(100) NOT NULL,
    license_plate character varying(10) NOT NULL
);


ALTER TABLE public.motorcycles OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 16399)
-- Name: motorcycles_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.motorcycles_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.motorcycles_id_seq OWNER TO postgres;

--
-- TOC entry 4932 (class 0 OID 0)
-- Dependencies: 219
-- Name: motorcycles_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.motorcycles_id_seq OWNED BY public.motorcycles.id;


--
-- TOC entry 221 (class 1259 OID 16410)
-- Name: schedules_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.schedules_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.schedules_id_seq OWNER TO postgres;

--
-- TOC entry 4933 (class 0 OID 0)
-- Dependencies: 221
-- Name: schedules_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.schedules_id_seq OWNED BY public.bookings.id;


--
-- TOC entry 4754 (class 2604 OID 16414)
-- Name: bookings id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.bookings ALTER COLUMN id SET DEFAULT nextval('public.schedules_id_seq'::regclass);


--
-- TOC entry 4752 (class 2604 OID 16392)
-- Name: couriers id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.couriers ALTER COLUMN id SET DEFAULT nextval('public.couriers_id_seq'::regclass);


--
-- TOC entry 4753 (class 2604 OID 16403)
-- Name: motorcycles id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.motorcycles ALTER COLUMN id SET DEFAULT nextval('public.motorcycles_id_seq'::regclass);


--
-- TOC entry 4925 (class 0 OID 16411)
-- Dependencies: 222
-- Data for Name: bookings; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.bookings (id, courier_id, motorcycle_id, start_date, end_date, expected_end_date, plan) FROM stdin;
2	motorista 2	moto 3	2025-06-30 21:09:14.148	2025-07-07 21:09:14.148	2025-07-07 21:09:14.148	7
4	motorista 4	moto 3	2025-06-30 21:19:18.304	2025-07-07 21:19:18.304	2025-07-07 21:19:18.304	7
5	motorista 4	moto 3	2025-06-30 21:19:18.304	2025-07-07 21:19:18.304	2025-07-07 21:19:18.304	7
1	motorista 1	moto 2	2025-06-30 20:37:54.966	2025-07-07 21:44:28.048	2025-07-30 20:37:54.966	30
\.


--
-- TOC entry 4921 (class 0 OID 16389)
-- Dependencies: 218
-- Data for Name: couriers; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.couriers (id, identifier, name, tax_id, birth_date, license_number, license_type, license_image) FROM stdin;
1	motorista 1	Gustavo Fernandes	99999999999   	1999-04-14	ABC1234	1 	base64string
3	motorista 2	Caio Lima	11111111111   	1999-04-14	ABC1235	2 	base64string
4	motorista 4	Vitor Gabriel	88888888888   	2000-06-30	123456789	0 	string
6	motorista 5	Ramiro Filho	44444444444   	2000-07-31	1478592356	0 	string
\.


--
-- TOC entry 4923 (class 0 OID 16400)
-- Dependencies: 220
-- Data for Name: motorcycles; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.motorcycles (id, identifier, year, model, license_plate) FROM stdin;
2	moto 2	2008	Honda	ABC0102
1	moto 1	2010	Yamaha	ABC1304
3	moto 3	2015	Kawasaki	GUT8039
\.


--
-- TOC entry 4934 (class 0 OID 0)
-- Dependencies: 217
-- Name: couriers_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.couriers_id_seq', 6, true);


--
-- TOC entry 4935 (class 0 OID 0)
-- Dependencies: 219
-- Name: motorcycles_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.motorcycles_id_seq', 5, true);


--
-- TOC entry 4936 (class 0 OID 0)
-- Dependencies: 221
-- Name: schedules_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.schedules_id_seq', 8, true);


--
-- TOC entry 4756 (class 2606 OID 16398)
-- Name: couriers couriers_identifier_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.couriers
    ADD CONSTRAINT couriers_identifier_key UNIQUE (identifier);


--
-- TOC entry 4758 (class 2606 OID 16396)
-- Name: couriers couriers_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.couriers
    ADD CONSTRAINT couriers_pkey PRIMARY KEY (id);


--
-- TOC entry 4764 (class 2606 OID 16407)
-- Name: motorcycles motorcycles_identifier_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.motorcycles
    ADD CONSTRAINT motorcycles_identifier_key UNIQUE (identifier);


--
-- TOC entry 4766 (class 2606 OID 16409)
-- Name: motorcycles motorcycles_license_plate_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.motorcycles
    ADD CONSTRAINT motorcycles_license_plate_key UNIQUE (license_plate);


--
-- TOC entry 4768 (class 2606 OID 16405)
-- Name: motorcycles motorcycles_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.motorcycles
    ADD CONSTRAINT motorcycles_pkey PRIMARY KEY (id);


--
-- TOC entry 4772 (class 2606 OID 16416)
-- Name: bookings schedules_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.bookings
    ADD CONSTRAINT schedules_pkey PRIMARY KEY (id);


--
-- TOC entry 4760 (class 2606 OID 16430)
-- Name: couriers unique_licensenumber; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.couriers
    ADD CONSTRAINT unique_licensenumber UNIQUE (license_number);


--
-- TOC entry 4770 (class 2606 OID 16432)
-- Name: motorcycles unique_licenseplate; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.motorcycles
    ADD CONSTRAINT unique_licenseplate UNIQUE (license_plate);


--
-- TOC entry 4762 (class 2606 OID 16428)
-- Name: couriers unique_taxid; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.couriers
    ADD CONSTRAINT unique_taxid UNIQUE (tax_id);


--
-- TOC entry 4773 (class 2606 OID 16417)
-- Name: bookings fk_courier; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.bookings
    ADD CONSTRAINT fk_courier FOREIGN KEY (courier_id) REFERENCES public.couriers(identifier);


--
-- TOC entry 4774 (class 2606 OID 16422)
-- Name: bookings fk_motorcycle; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.bookings
    ADD CONSTRAINT fk_motorcycle FOREIGN KEY (motorcycle_id) REFERENCES public.motorcycles(identifier);


-- Completed on 2025-06-30 23:25:33

--
-- PostgreSQL database dump complete
--

