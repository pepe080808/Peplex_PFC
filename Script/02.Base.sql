CREATE PROCEDURE DDLVersion
       @Version VARCHAR(16) OUT
AS
BEGIN
  SET @Version = 'v_0_0_7_9';
END;

GO

DECLARE @SiteId VARCHAR(3);
SELECT @SiteId = CAST(CAST(RAND() * 100 AS INT) AS VARCHAR(3));

DECLARE @sqlCommand varchar(1000);
SET @sqlCommand = 'CREATE PROCEDURE SiteId @SiteId INT OUT AS BEGIN SET @SiteId = ' + @SiteId + '; END;'
EXEC (@sqlCommand)

GO

CREATE TABLE LastDocumentId
(
		Id					VARCHAR(32) NOT NULL,
		LastUsedNumber		INT NOT NULL,

		CONSTRAINT PK_LastDocumentId PRIMARY KEY(Id)
);

CREATE TYPE IntFeedTable AS TABLE
(
    Value int NOT NULL,

	PRIMARY KEY NONCLUSTERED(Value)
);

GO

CREATE TABLE ItemFilterManagement
(
		Id				int identity(1,1) not null,
		Employee		int not null,
		EntryType		int not null, /* 0=Recently used, 1=Explicitly saved by user */
		EntryDateTime	datetime not null,
		Name			varchar(100) not null,
		FilterXml		xml not null,

		CONSTRAINT PK_ItemFilterManagement PRIMARY KEY CLUSTERED (Id)
);

GO

CREATE INDEX ItemFilterManagement_Employee_EntryType_EntryDateTime ON ItemFilterManagement(Employee, EntryType, EntryDateTime);
CREATE INDEX ItemFilterManagement_Employee_EntryType_Name_EntryDateTime ON ItemFilterManagement(Employee, EntryType, Name, EntryDateTime);

GO

CREATE TABLE HeadofficeSettings
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
    	Id						INT NOT NULL,
    	Name					VARCHAR (80),
		XmlContents				XML NOT NULL,

		CONSTRAINT PK_HeadofficeSettings PRIMARY KEY CLUSTERED (Id)
);

CREATE INDEX HeadofficeSettings_RecId ON HeadofficeSettings(RecId);

CREATE TABLE ItemGroup01
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
    	Id						VARCHAR (10) NOT NULL,
    	Name					VARCHAR (32),

		CONSTRAINT PK_ItemGroup01 PRIMARY KEY CLUSTERED (Id)
);

CREATE INDEX ItemGroup01_RecId ON ItemGroup01(RecId);

CREATE TABLE ItemGroup02
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
    	Id						VARCHAR (10) NOT NULL,
    	Name					VARCHAR (32),

		CONSTRAINT PK_ItemGroup02 PRIMARY KEY CLUSTERED (Id)
);

CREATE INDEX ItemGroup02_RecId ON ItemGroup02(RecId);

CREATE TABLE ItemGroup03
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
    	Id						VARCHAR (10) NOT NULL,
    	Name					VARCHAR (32),

		CONSTRAINT PK_ItemGroup03 PRIMARY KEY CLUSTERED (Id)
);

CREATE INDEX ItemGroup03_RecId ON ItemGroup03(RecId);

CREATE TABLE ItemGroup04
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
    	Id						VARCHAR (10) NOT NULL,
    	Name					VARCHAR (32),

		CONSTRAINT PK_ItemGroup04 PRIMARY KEY CLUSTERED (Id)
);

CREATE INDEX ItemGroup04_RecId ON ItemGroup04(RecId);

CREATE TABLE ItemGroup05
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
    	Id						VARCHAR (10) NOT NULL,
    	Name					VARCHAR (32),

		CONSTRAINT PK_ItemGroup05 PRIMARY KEY CLUSTERED (Id)
);

CREATE INDEX ItemGroup05_RecId ON ItemGroup05(RecId);

CREATE TABLE ItemGroup06
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
    	Id						VARCHAR (10) NOT NULL,
    	Name					VARCHAR (32),

		CONSTRAINT PK_ItemGroup06 PRIMARY KEY CLUSTERED (Id)
);

CREATE INDEX ItemGroup06_RecId ON ItemGroup06(RecId);

CREATE TABLE ItemGroup07
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
    	Id						VARCHAR (10) NOT NULL,
    	Name					VARCHAR (32),

		CONSTRAINT PK_ItemGroup07 PRIMARY KEY CLUSTERED (Id)
);

CREATE INDEX ItemGroup07_RecId ON ItemGroup07(RecId);

CREATE TABLE ItemGroup08
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
    	Id						VARCHAR (10) NOT NULL,
    	Name					VARCHAR (32),

		CONSTRAINT PK_ItemGroup08 PRIMARY KEY CLUSTERED (Id)
);

CREATE INDEX ItemGroup08_RecId ON ItemGroup08(RecId);

CREATE TABLE ItemGroup09
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
    	Id						VARCHAR (10) NOT NULL,
    	Name					VARCHAR (32),

		CONSTRAINT PK_ItemGroup09 PRIMARY KEY CLUSTERED (Id)
);

CREATE INDEX ItemGroup09_RecId ON ItemGroup09(RecId);

CREATE TABLE ItemGroup10
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
    	Id						VARCHAR (10) NOT NULL,
    	Name					VARCHAR (32),

		CONSTRAINT PK_ItemGroup10 PRIMARY KEY CLUSTERED (Id)
);

CREATE INDEX ItemGroup10_RecId ON ItemGroup10(RecId);

CREATE TABLE ItemGroup11
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
    	Id						VARCHAR (10) NOT NULL,
    	Name					VARCHAR (32),

		CONSTRAINT PK_ItemGroup11 PRIMARY KEY CLUSTERED (Id)
);

CREATE INDEX ItemGroup11_RecId ON ItemGroup11(RecId);

CREATE TABLE ItemGroup12
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
    	Id						VARCHAR (10) NOT NULL,
    	Name					VARCHAR (32),

		CONSTRAINT PK_ItemGroup12 PRIMARY KEY CLUSTERED (Id)
);

CREATE INDEX ItemGroup12_RecId ON ItemGroup12(RecId);

CREATE TABLE ItemGroup13
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
    	Id						VARCHAR (10) NOT NULL,
    	Name					VARCHAR (32),

		CONSTRAINT PK_ItemGroup13 PRIMARY KEY CLUSTERED (Id)
);

CREATE INDEX ItemGroup13_RecId ON ItemGroup13(RecId);

CREATE TABLE ItemGroup14
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
    	Id						VARCHAR (10) NOT NULL,
    	Name					VARCHAR (32),

		CONSTRAINT PK_ItemGroup14 PRIMARY KEY CLUSTERED (Id)
);

CREATE INDEX ItemGroup14_RecId ON ItemGroup14(RecId);

CREATE TABLE Color
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
    	Id						VARCHAR (10) NOT NULL,
    	Name					VARCHAR (32),

		CONSTRAINT PK_Color PRIMARY KEY CLUSTERED (Id)
);

CREATE INDEX Color_RecId ON Color(RecId);

CREATE TABLE Country
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
    	Id						VARCHAR (10) NOT NULL,
    	Name					VARCHAR (32),
		Bitmap					varbinary(max),

		CONSTRAINT PK_Country PRIMARY KEY CLUSTERED (Id)
);

CREATE INDEX Country_RecId ON Country(RecId);

CREATE TABLE Company
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
		Id						INT NOT NULL,
		Name					VARCHAR (64),
		FiscalId				VARCHAR (32),
		Address1				VARCHAR (64),
		Address2				VARCHAR (64),
		ZipCode					VARCHAR (16),
		City					VARCHAR (32),
		Province				VARCHAR (32),
		CountryId				VARCHAR (10) NOT NULL,
		Phone					VARCHAR (32),
		EMail					VARCHAR (64),
		Logo					varbinary(max),

		CONSTRAINT PK_Company PRIMARY KEY CLUSTERED (Id),
		CONSTRAINT FK_Company_CountryId FOREIGN KEY(CountryId) REFERENCES Country(Id)
);

CREATE INDEX Company_RecId ON Company(RecId);

GO

CREATE TABLE Currency
(
		RecId				INT IDENTITY(1,1),
		RecVersion			rowversion,
		Id					VARCHAR(10) NOT NULL,
		Name				VARCHAR(32),
		Rate				FLOAT NOT NULL,
		AbbrSymbol			VARCHAR(10) NOT NULL,
		Decimals			INT NOT NULL,

		CONSTRAINT PK_Currency PRIMARY KEY CLUSTERED (Id)
);

CREATE INDEX Currency_RecId ON Currency(RecId);

CREATE TABLE SaleTariffRegion
(
		RecId				INT IDENTITY(1,1),
		RecVersion			rowversion,
		Id					VARCHAR(10) NOT NULL,
		Name				VARCHAR(64),
		CurrencyId			VARCHAR(10) NOT NULL,
		Bitmap				VARBINARY(MAX),

		CONSTRAINT PK_SaleTariffRegion PRIMARY KEY CLUSTERED (Id),
		CONSTRAINT FK_SaleTariffRegion_CurrencyId FOREIGN KEY(CurrencyId) REFERENCES Currency(Id)
);

CREATE INDEX SaleTariffRegion_RecId ON SaleTariffRegion(RecId);

GO

CREATE TABLE Language
(
		RecId						INT IDENTITY(1,1),
		RecVersion					rowversion,
    	Id							VARCHAR (10) NOT NULL,
    	Name						VARCHAR (32),
		IsExternalPlatformLanguage	BIT NOT NULL,

		CONSTRAINT PK_Language PRIMARY KEY CLUSTERED (Id)
);

CREATE INDEX Language_RecId ON Language(RecId);

CREATE TABLE Title
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
    	Id						INT NOT NULL,
    	Name					VARCHAR (32),

		CONSTRAINT PK_Title PRIMARY KEY CLUSTERED (Id)
);

CREATE INDEX Title_RecId ON Title(RecId);

GO

CREATE TABLE MailingCode01
(
		RecId						INT IDENTITY(1,1),
		RecVersion					rowversion,
		Id							INT NOT NULL,
		Name						VARCHAR(32) NOT NULL,
		
		CONSTRAINT PK_MailingCode01 PRIMARY KEY CLUSTERED (Id)
);

CREATE INDEX MailingCode01_RecId ON MailingCode01(RecId);

CREATE TABLE MailingCode02
(
		RecId						INT IDENTITY(1,1),
		RecVersion					rowversion,
		Id							INT NOT NULL,
		Name						VARCHAR(32) NOT NULL,
		
		CONSTRAINT PK_MailingCode02 PRIMARY KEY CLUSTERED (Id)
);

CREATE INDEX MailingCode02_RecId ON MailingCode02(RecId);

CREATE TABLE MailingCode03
(
		RecId						INT IDENTITY(1,1),
		RecVersion					rowversion,
		Id							INT NOT NULL,
		Name						VARCHAR(32) NOT NULL,
		
		CONSTRAINT PK_MailingCode03 PRIMARY KEY CLUSTERED (Id)
);

CREATE INDEX MailingCode03_RecId ON MailingCode03(RecId);

CREATE TABLE MailingCode04
(
		RecId						INT IDENTITY(1,1),
		RecVersion					rowversion,
		Id							INT NOT NULL,
		Name						VARCHAR(32) NOT NULL,
		
		CONSTRAINT PK_MailingCode04 PRIMARY KEY CLUSTERED (Id)
);

CREATE INDEX MailingCode04_RecId ON MailingCode04(RecId);

CREATE TABLE MailingCode05
(
		RecId						INT IDENTITY(1,1),
		RecVersion					rowversion,
		Id							INT NOT NULL,
		Name						VARCHAR(32) NOT NULL,
		
		CONSTRAINT PK_MailingCode05 PRIMARY KEY CLUSTERED (Id)
);

CREATE INDEX MailingCode05_RecId ON MailingCode05(RecId);

CREATE TABLE MailingCode06
(
		RecId						INT IDENTITY(1,1),
		RecVersion					rowversion,
		Id							INT NOT NULL,
		Name						VARCHAR(32) NOT NULL,
		
		CONSTRAINT PK_MailingCode06 PRIMARY KEY CLUSTERED (Id)
);

CREATE INDEX MailingCode06_RecId ON MailingCode06(RecId);

GO

CREATE TABLE DiscountReasonGroup
(
		RecId						INT IDENTITY(1,1),
		RecVersion					rowversion,
		Id							INT NOT NULL,
		Position					INT NOT NULL,	
		Name						VARCHAR(64),
		GroupType					TINYINT NOT NULL, /* 0=Line discounts, 1=Total discounts */
		Visible						BIT NOT NULL,

		CONSTRAINT PK_DiscountReasonGroup PRIMARY KEY(Id)
);

CREATE INDEX DiscountReasonGroup_RecId ON DiscountReasonGroup(RecId);

CREATE TABLE DiscountReason
(
		RecId						INT IDENTITY(1,1),
		RecVersion					rowversion,
		Id							INT NOT NULL,
		GroupId						INT NOT NULL,
		Position					INT NOT NULL,	
		Name						VARCHAR(64),
		Internal					BIT NOT NULL,
		ValueType					TINYINT NOT NULL, /* 0=Absolute, 1=Percentage */
		SuggestValue				BIT NOT NULL,
		SuggestedValue				DECIMAL (9, 2),
		SuggestedValueCanAlter		BIT NOT NULL,
		SuggestedValueIsFinalPrice	BIT NOT NULL,
		CalculateOverBase 			BIT NOT NULL, 
		IsAutoLineDiscount          BIT NOT NULL, 
		ActiveMode					TINYINT NOT NULL,
		ActiveFromDate				DATETIME,
		ActiveToDate				DATETIME,
		RequiresExternalRef 		BIT NOT NULL,
		RequiresEmployee			BIT NOT NULL,
		RequiresVoucherId			BIT NOT NULL,
		VoucherValidityCheck		BIT NOT NULL,

		CONSTRAINT PK_DiscountReason PRIMARY KEY(Id),
		CONSTRAINT FK_DiscountReason_DiscountReasonGroup FOREIGN KEY(GroupId) REFERENCES DiscountReasonGroup(Id)
);

CREATE INDEX DiscountReason_RecId ON DiscountReason(RecId);

CREATE TABLE DiscountReasonIncompatibility
(
		RecId						INT IDENTITY(1,1),
		RecVersion					rowversion,
		DiscountReasonId			INT NOT NULL,
		IncompatibleReasonId		INT NOT NULL,

		CONSTRAINT PK_DiscountReasonIncompatibility PRIMARY KEY(DiscountReasonId, IncompatibleReasonId),
		CONSTRAINT FK_DiscountReasonIncompatibility_DiscountReasonId FOREIGN KEY(DiscountReasonId) REFERENCES DiscountReason(Id) ON DELETE CASCADE,
		CONSTRAINT FK_DiscountReasonIncompatibility_IncompatibleReasonId FOREIGN KEY(IncompatibleReasonId) REFERENCES DiscountReason(Id)
);

CREATE INDEX DiscountReasonIncompatibility_RecId ON DiscountReasonIncompatibility(RecId);

GO

CREATE TABLE Customer
(
		RecId						INT IDENTITY(1,1),
		RecVersion					rowversion,
		Id							BIGINT NOT NULL,
		ShopId						INT NOT NULL,
		TerminalId					INT NOT NULL,
		Number						INT NOT NULL,
		CompanyName					VARCHAR(64),
		TitleId						INT,
		Name						VARCHAR(64),
		Surname1					VARCHAR(64),
		Surname2					VARCHAR(64),
		FiscalNumber				VARCHAR(20),
		Address1					VARCHAR(64),
		Address2					VARCHAR(64),
		ZipCode						VARCHAR(15),
		City						VARCHAR(32),
		Province					VARCHAR(32),
		CountryId					VARCHAR(10),
		Phone1						VARCHAR(25),
		Phone2						VARCHAR(25),
		EMail						VARCHAR(64),
		LanguageId					VARCHAR(10),
		MailingCode01Id				INT,
		MailingCode02Id				INT,
		MailingCode03Id				INT,
		MailingCode04Id				INT,
		MailingCode05Id				INT,
		MailingCode06Id				INT,
		BirthDate					DATETIME,
		CustomerCardNumber			VARCHAR(20),
		LoyaltyActive				BIT NOT NULL DEFAULT 0,
		LoyaltySavingsPercentage	DECIMAL(5, 2) NOT NULL DEFAULT 0,
		LoyaltyBalance				DECIMAL(9, 2) NOT NULL DEFAULT 0,
		LoyaltyBalanceLastModified	DATETIME,
		DiscountReasonId			INT,

		CONSTRAINT PK_Customer PRIMARY KEY CLUSTERED (Id),
		CONSTRAINT FK_Customer_CountryId FOREIGN KEY(CountryId) REFERENCES Country(Id),
		CONSTRAINT FK_Customer_TitleId FOREIGN KEY(TitleId) REFERENCES Title(Id),
		CONSTRAINT FK_Customer_LanguageId FOREIGN KEY(LanguageId) REFERENCES Language(Id),
		CONSTRAINT FK_Customer_MailingCode01Id FOREIGN KEY(MailingCode01Id) REFERENCES MailingCode01(Id),
		CONSTRAINT FK_Customer_MailingCode02Id FOREIGN KEY(MailingCode02Id) REFERENCES MailingCode02(Id),
		CONSTRAINT FK_Customer_MailingCode03Id FOREIGN KEY(MailingCode03Id) REFERENCES MailingCode03(Id),
		CONSTRAINT FK_Customer_MailingCode04Id FOREIGN KEY(MailingCode04Id) REFERENCES MailingCode04(Id),
		CONSTRAINT FK_Customer_MailingCode05Id FOREIGN KEY(MailingCode05Id) REFERENCES MailingCode05(Id),
		CONSTRAINT FK_Customer_MailingCode06Id FOREIGN KEY(MailingCode06Id) REFERENCES MailingCode06(Id),
		CONSTRAINT FK_Customer_DiscountReasonId FOREIGN KEY(DiscountReasonId) REFERENCES DiscountReason(Id)
);
 
CREATE INDEX Customer_RecId ON Customer(RecId);
CREATE INDEX Customer_ShopId_TerminalId_Number ON Customer(ShopId, TerminalId, Number);
CREATE INDEX Customer_CustomerCardNumber ON Customer(CustomerCardNumber);

CREATE TABLE CustomerShippingAddress
(
		RecId				INT IDENTITY(1,1),
		RecVersion			rowversion,
		CustomerId			BIGINT NOT NULL,
		Name				VARCHAR(64),
		Surname1			VARCHAR(64),
		Surname2			VARCHAR(64),
		Address1			VARCHAR(64),
		Address2			VARCHAR(64),
		ZipCode				VARCHAR(15),
		City				VARCHAR(32),
		Province			VARCHAR(32),
		CountryId			VARCHAR(10),
		Phone1				VARCHAR(25),
		Phone2				VARCHAR(25),
		
		CONSTRAINT PK_CustomerShippingAddress PRIMARY KEY CLUSTERED (RecId),
		CONSTRAINT FK_CustomerShippingAddress_CustomerId FOREIGN KEY(CustomerId) REFERENCES Customer(Id) ON DELETE CASCADE,
		CONSTRAINT FK_CustomerShippingAddress_CountryId FOREIGN KEY(CountryId) REFERENCES Country(Id)
);

CREATE INDEX CustomerShippingAddress_CustomerId ON CustomerShippingAddress(CustomerId);

GO

CREATE TABLE ShopGroup
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
		Id						INT NOT NULL,
		Name					VARCHAR (32) NOT NULL,

		CONSTRAINT PK_ShopGroup PRIMARY KEY CLUSTERED (Id),
);

CREATE INDEX ShopGroup_RecId ON ShopGroup(RecId);

GO

CREATE TABLE Shop
(
		RecId						INT IDENTITY(1,1),
		RecVersion					rowversion,
		Id							INT NOT NULL,
		ShopGroupId					INT NOT NULL,
		CompanyId					INT NOT NULL,
		Name						VARCHAR (32) NOT NULL,
		Address1					VARCHAR (64),
		Address2					VARCHAR (64),
		ZipCode						VARCHAR (16),
		City						VARCHAR (32),
		Province					VARCHAR (32),
		CountryId					VARCHAR (10) NOT NULL,
		Phone						VARCHAR (32),
		EMail						VARCHAR (64),
		SaleTariffRegionId			VARCHAR (10) NOT NULL,
		DefaultCustomerId			BIGINT NOT NULL,
		IsHeadoffice				BIT NOT NULL,
		IsDeliveryRequestHc			BIT NOT NULL,
		ReportVariables				XML,
		ItemSearchColumnsSort		XML,
		ItemInfoVisibleShops		XML,
		ShopGln						VARCHAR(13),
		SaleTaxId1					INT NOT NULL,
		SaleTaxId2					INT NOT NULL,
		SaleTaxId3					INT NOT NULL,
		PresenceControlEnabled		BIT NOT NULL DEFAULT 0,
		PresenceControlMoExpireTime DATETIME,
		PresenceControlTuExpireTime DATETIME,
		PresenceControlWeExpireTime DATETIME,
		PresenceControlThExpireTime DATETIME,
		PresenceControlFrExpireTime DATETIME,
		PresenceControlSaExpireTime DATETIME,
		PresenceControlSuExpireTime DATETIME,
		IsAutoReplenishmentSourceShop		BIT NOT NULL DEFAULT 1,
		IsAutoReplenishmentDestinationShop	BIT NOT NULL DEFAULT 1,

		CONSTRAINT PK_Shop PRIMARY KEY CLUSTERED (Id),
		CONSTRAINT FK_Shop_CompanyId FOREIGN KEY(CompanyId) REFERENCES Company(Id),
		CONSTRAINT FK_Shop_CountryId FOREIGN KEY(CountryId) REFERENCES Country(Id),
		CONSTRAINT FK_Shop_SaleTariffRegionId FOREIGN KEY(SaleTariffRegionId) REFERENCES SaleTariffRegion(Id),
		CONSTRAINT FK_Shop_DefaultCustomerId FOREIGN KEY(DefaultCustomerId) REFERENCES Customer(Id),
		CONSTRAINT FK_Shop_ShopGroupId FOREIGN KEY(ShopGroupId) REFERENCES ShopGroup(Id)
);

CREATE INDEX Shop_RecId ON Shop(RecId);

GO

CREATE TABLE ShopDatabaseIds
(
		ShopId				INT NOT NULL,
		TerminalId			INT NOT NULL,
		DatabaseId			VARCHAR(36),
		RegisterDateTime	DATETIME NOT NULL,
		SoftwareVersion 	VARCHAR(16),
		SoftwareVersionDate	DATETIME,

		CONSTRAINT PK_ShopDatabaseIds PRIMARY KEY CLUSTERED (ShopId, TerminalId),
);

GO

CREATE TABLE CommunicationsTracking
(
		ShopId							int NOT NULL,
		TerminalId						int NOT NULL,
		DatabaseId						varchar(36) NOT NULL,
		LastProcessedTrackingId			int NOT NULL,
		LastProcessedTrackingDateTime	datetime NOT NULL,
		
		CONSTRAINT PK_CommunicationsTracking PRIMARY KEY CLUSTERED (ShopId, TerminalId, DatabaseId),
		CONSTRAINT FK_CommunicationsTracking_ShopId FOREIGN KEY(ShopId) REFERENCES Shop(Id) ON DELETE CASCADE
);

GO

CREATE TABLE LastProcessedSequence
(
		ShopId			INT NOT NULL,
		Sequence		INT NOT NULL,

		CONSTRAINT PK_LastProcessedSequence PRIMARY KEY CLUSTERED (ShopId, Sequence),
		CONSTRAINT FK_LastProcessedSequence_ShopId FOREIGN KEY(ShopId) REFERENCES Shop(Id) ON DELETE CASCADE
);

CREATE TABLE Season
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
    	Id						VARCHAR (10) NOT NULL,
    	Name					VARCHAR (32),
		Obsolete				BIT NOT NULL,

		CONSTRAINT PK_Season PRIMARY KEY CLUSTERED (Id)
);

CREATE INDEX Season_RecId ON Season(RecId);

GO

CREATE TABLE SizeDomain
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
    	Id						VARCHAR (10) NOT NULL,
    	Name					VARCHAR (32),
		IntSizeLabel00			VARCHAR (10),
		IntSizeLabel01			VARCHAR (10),
		IntSizeLabel02			VARCHAR (10),
		IntSizeLabel03			VARCHAR (10),
		IntSizeLabel04			VARCHAR (10),
		IntSizeLabel05			VARCHAR (10),
		IntSizeLabel06			VARCHAR (10),
		IntSizeLabel07			VARCHAR (10),
		IntSizeLabel08			VARCHAR (10),
		IntSizeLabel09			VARCHAR (10),
		IntSizeLabel10			VARCHAR (10),
		IntSizeLabel11			VARCHAR (10),
		IntSizeLabel12			VARCHAR (10),
		IntSizeLabel13			VARCHAR (10),
		IntSizeLabel14			VARCHAR (10),
		IntSizeLabel15			VARCHAR (10),
		IntSizeLabel16			VARCHAR (10),
		IntSizeLabel17			VARCHAR (10),
		IntSizeLabel18			VARCHAR (10),
		IntSizeLabel19			VARCHAR (10),
		IntSizeLabel20			VARCHAR (10),
		IntSizeLabel21			VARCHAR (10),
		IntSizeLabel22			VARCHAR (10),
		IntSizeLabel23			VARCHAR (10),
		IntSizeLabel24			VARCHAR (10),
		IntSizeLabel25			VARCHAR (10),
		Obsolete				BIT NOT NULL,

		CONSTRAINT PK_Sizedomain PRIMARY KEY CLUSTERED (Id)
);

CREATE INDEX SizeDomain_RecId ON SizeDomain(RecId);

CREATE TABLE SizeDomainConversion
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
    	SizeDomainId			VARCHAR (10) NOT NULL,
    	Id						VARCHAR (10) NOT NULL,
    	Name					VARCHAR (32),
		ExtSizeLabel00			VARCHAR (10),
		ExtSizeLabel01			VARCHAR (10),
		ExtSizeLabel02			VARCHAR (10),
		ExtSizeLabel03			VARCHAR (10),
		ExtSizeLabel04			VARCHAR (10),
		ExtSizeLabel05			VARCHAR (10),
		ExtSizeLabel06			VARCHAR (10),
		ExtSizeLabel07			VARCHAR (10),
		ExtSizeLabel08			VARCHAR (10),
		ExtSizeLabel09			VARCHAR (10),
		ExtSizeLabel10			VARCHAR (10),
		ExtSizeLabel11			VARCHAR (10),
		ExtSizeLabel12			VARCHAR (10),
		ExtSizeLabel13			VARCHAR (10),
		ExtSizeLabel14			VARCHAR (10),
		ExtSizeLabel15			VARCHAR (10),
		ExtSizeLabel16			VARCHAR (10),
		ExtSizeLabel17			VARCHAR (10),
		ExtSizeLabel18			VARCHAR (10),
		ExtSizeLabel19			VARCHAR (10),
		ExtSizeLabel20			VARCHAR (10),
		ExtSizeLabel21			VARCHAR (10),
		ExtSizeLabel22			VARCHAR (10),
		ExtSizeLabel23			VARCHAR (10),
		ExtSizeLabel24			VARCHAR (10),
		ExtSizeLabel25			VARCHAR (10),

		CONSTRAINT PK_SizedomainConversion PRIMARY KEY CLUSTERED (SizeDomainId, Id),
		CONSTRAINT FK_SizedomainConversion_SizeDomainId FOREIGN KEY(SizeDomainId) REFERENCES SizeDomain(Id) ON DELETE CASCADE
);

CREATE INDEX SizeDomainConversion_RecId ON SizeDomainConversion(RecId);

CREATE TABLE SizeDomainMinimumTemplate
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
    	SizeDomainId			VARCHAR (10) NOT NULL,
    	Id						VARCHAR (10) NOT NULL,
		Quantity00				INT NOT NULL,
		Quantity01				INT NOT NULL,
		Quantity02				INT NOT NULL,
		Quantity03				INT NOT NULL,
		Quantity04				INT NOT NULL,
		Quantity05				INT NOT NULL,
		Quantity06				INT NOT NULL,
		Quantity07				INT NOT NULL,
		Quantity08				INT NOT NULL,
		Quantity09				INT NOT NULL,
		Quantity10				INT NOT NULL,
		Quantity11				INT NOT NULL,
		Quantity12				INT NOT NULL,
		Quantity13				INT NOT NULL,
		Quantity14				INT NOT NULL,
		Quantity15				INT NOT NULL,
		Quantity16				INT NOT NULL,
		Quantity17				INT NOT NULL,
		Quantity18				INT NOT NULL,
		Quantity19				INT NOT NULL,
		Quantity20				INT NOT NULL,
		Quantity21				INT NOT NULL,
		Quantity22				INT NOT NULL,
		Quantity23				INT NOT NULL,
		Quantity24				INT NOT NULL,
		Quantity25				INT NOT NULL,

		CONSTRAINT PK_SizeDomainMinimumTemplate PRIMARY KEY CLUSTERED (SizeDomainId, Id),
		CONSTRAINT FK_SizeDomainMinimumTemplate_SizeDomainId FOREIGN KEY(SizeDomainId) REFERENCES SizeDomain(Id) ON DELETE CASCADE
);

CREATE INDEX SizeDomainMinimumTemplate_RecId ON SizeDomainMinimumTemplate(RecId);

CREATE TABLE Tax
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
		Id						INT NOT NULL,
		Name					VARCHAR(32),
		Percentage				DECIMAL(7,3) NOT NULL,

		CONSTRAINT PK_Tax PRIMARY KEY CLUSTERED (Id)
);

CREATE INDEX Tax_RecId ON Tax(RecId);

CREATE TABLE Supplier
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
		Id						INT NOT NULL,
		SupplierName			VARCHAR(64),
		SupplierFiscalNumber	VARCHAR(20),
		SupplierAddress1		VARCHAR(64),
		SupplierAddress2		VARCHAR(64),
		SupplierZipCode			VARCHAR(15),
		SupplierCity			VARCHAR(32),
		SupplierProvince		VARCHAR(32),
		SupplierCountryId		VARCHAR(10),
		SupplierPhone1			VARCHAR(25),
		SupplierPhone2			VARCHAR(25),
		SupplierEMail			VARCHAR(64),
		SupplierUrl				VARCHAR(64),
		SupplierGln				VARCHAR(13),
		CurrencyId				VARCHAR(10) NOT NULL,
		PurchaseTaxId			INT NOT NULL,

		CONSTRAINT PK_Supplier PRIMARY KEY CLUSTERED (Id),
		CONSTRAINT FK_Supplier_SupplierCountryId FOREIGN KEY(SupplierCountryId) REFERENCES Country(Id),
		CONSTRAINT FK_Supplier_CurrencyId FOREIGN KEY(CurrencyId) REFERENCES Currency(Id),
		CONSTRAINT FK_Supplier_PurchaseTaxId FOREIGN KEY(PurchaseTaxId) REFERENCES Tax(Id)
);

CREATE INDEX Supplier_RecId ON Supplier(RecId);

CREATE TABLE Brand
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
		Id						INT NOT NULL,
		Name					VARCHAR(80) NOT NULL,
		NameOnBarcodeLabel		VARCHAR(80),
		Obsolete				BIT NOT NULL,
		SupplierId				INT NOT NULL,
		RetailPriceFactor		DECIMAL(4,2),
		ItemGroup01DefaultId	VARCHAR (10),
		ItemGroup02DefaultId	VARCHAR (10),
		ItemGroup03DefaultId	VARCHAR (10),
		ItemGroup04DefaultId	VARCHAR (10),
		ItemGroup05DefaultId	VARCHAR (10),
		ItemGroup06DefaultId	VARCHAR (10),
		ItemGroup07DefaultId	VARCHAR (10),
		ItemGroup08DefaultId	VARCHAR (10),
		ItemGroup09DefaultId	VARCHAR (10),
		ItemGroup10DefaultId	VARCHAR (10),
		ItemGroup11DefaultId	VARCHAR (10),
		ItemGroup12DefaultId	VARCHAR (10),
		ItemGroup13DefaultId	VARCHAR (10),
		ItemGroup14DefaultId	VARCHAR (10),

		CONSTRAINT PK_Brand PRIMARY KEY CLUSTERED (Id),
		CONSTRAINT FK_Brand_SupplierId FOREIGN KEY(SupplierId) REFERENCES Supplier(Id)
);

CREATE INDEX Brand_RecId ON Brand(RecId);

CREATE TABLE BrandSizeDomain
(
	BrandId					INT NOT NULL,
	SizeDomainId			VARCHAR(10) NOT NULL,
	SizeDomainConversionId	VARCHAR(10) NOT NULL,

	CONSTRAINT PK_BrandSizeDomain PRIMARY KEY (BrandId, SizeDomainId, SizeDomainConversionId),
	CONSTRAINT FK_BrandSizeDomain_BrandId FOREIGN KEY (BrandId) REFERENCES Brand(Id) ON DELETE CASCADE,
	CONSTRAINT FK_BrandSizeDomain_SizeDomainId FOREIGN KEY (SizeDomainId) REFERENCES SizeDomain(Id),
	CONSTRAINT FK_BrandSizeDomain_SizeDomainConversionId FOREIGN KEY (SizeDomainId, SizeDomainConversionId) REFERENCES SizeDomainConversion(SizeDomainId, Id)
);

CREATE TABLE Item
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
		Id						INT NOT NULL,
		Style					VARCHAR(64),
		BrandId					INT NOT NULL,
		ColorId					VARCHAR(10) NOT NULL,
		SeasonId				VARCHAR(10) NOT NULL,
		SizeDomainId			VARCHAR(10) NOT NULL,
		SizeDomainConversionId	VARCHAR(10) NOT NULL,
		ItemGroup01Id			VARCHAR(10) NOT NULL,
		ItemGroup02Id			VARCHAR(10) NOT NULL,
		ItemGroup03Id			VARCHAR(10) NOT NULL,
		ItemGroup04Id			VARCHAR(10) NOT NULL,
		ItemGroup05Id			VARCHAR(10) NOT NULL,
		ItemGroup06Id			VARCHAR(10) NOT NULL,
		ItemGroup07Id			VARCHAR(10) NOT NULL,
		ItemGroup08Id			VARCHAR(10) NOT NULL,
		ItemGroup09Id			VARCHAR(10) NOT NULL,
		ItemGroup10Id			VARCHAR(10) NOT NULL,
		ItemGroup11Id			VARCHAR(10) NOT NULL,
		ItemGroup12Id			VARCHAR(10) NOT NULL,
		ItemGroup13Id			VARCHAR(10) NOT NULL,
		ItemGroup14Id			VARCHAR(10) NOT NULL,
		SaleTaxId				INT NOT NULL,
		SupplierInfo1			VARCHAR(64),
		SupplierInfo2			VARCHAR(64),
		SupplierInfo3			VARCHAR(64),
		SupplierInfo4			VARCHAR(64),
		SupplierInfo5			VARCHAR(64),
		SupplierInfo6			VARCHAR(64),
		SupplierInfo7			VARCHAR(64),
		SupplierInfo8			VARCHAR(64),
		SupplierNote			VARCHAR(256),
		SizeRanges				TINYINT NOT NULL,
		SizeSplit1				TINYINT NOT NULL,
		SizeSplit2				TINYINT NOT NULL,
		SizeSplit3				TINYINT NOT NULL,
		SizeSplit4				TINYINT NOT NULL,
		SizeSplit5				TINYINT NOT NULL,
		SizeSplit6				TINYINT NOT NULL,
		SizeSplit7				TINYINT NOT NULL,
		SizeSplit8				TINYINT NOT NULL,
		PurchasePrice1			DECIMAL(9,2) NOT NULL,
		PurchasePrice2			DECIMAL(9,2) NOT NULL,
		PurchasePrice3			DECIMAL(9,2) NOT NULL,
		PurchasePrice4			DECIMAL(9,2) NOT NULL,
		PurchasePrice5			DECIMAL(9,2) NOT NULL,
		PurchasePrice6			DECIMAL(9,2) NOT NULL,
		PurchasePrice7			DECIMAL(9,2) NOT NULL,
		PurchasePrice8			DECIMAL(9,2) NOT NULL,
		FormulaField			VARCHAR(128),
		ProhibitDiscount		BIT DEFAULT 0,
		LinkedItemId 			INT,
		ExternalPlatformTitleDefaultLanguage VARCHAR(255),
		ProhibitItemMovementRequests BIT NOT NULL DEFAULT 0,
        InternalPrice1			decimal(9,2) NOT NULL DEFAULT 0,
        InternalPrice2			decimal(9,2) NOT NULL DEFAULT 0,
        InternalPrice3			decimal(9,2) NOT NULL DEFAULT 0,
        InternalPrice4			decimal(9,2) NOT NULL DEFAULT 0,
        InternalPrice5			decimal(9,2) NOT NULL DEFAULT 0,
        InternalPrice6			decimal(9,2) NOT NULL DEFAULT 0,
        InternalPrice7			decimal(9,2) NOT NULL DEFAULT 0,
        InternalPrice8			decimal(9,2) NOT NULL DEFAULT 0,

		CONSTRAINT PK_Item PRIMARY KEY CLUSTERED (Id),
		CONSTRAINT FK_Item_BrandId FOREIGN KEY(BrandId) REFERENCES Brand(Id),
		CONSTRAINT FK_Item_ColorId FOREIGN KEY(ColorId) REFERENCES Color(Id),
		CONSTRAINT FK_Item_SeasonId FOREIGN KEY(SeasonId) REFERENCES Season(Id),
		CONSTRAINT FK_Item_SizeDomainId FOREIGN KEY(SizeDomainId) REFERENCES SizeDomain(Id),
		CONSTRAINT FK_Item_SizeDomainConversionId FOREIGN KEY(SizeDomainId, SizeDomainConversionId) REFERENCES SizeDomainConversion(SizeDomainId, Id),
		CONSTRAINT FK_Item_ItemGroup01Id FOREIGN KEY(ItemGroup01Id) REFERENCES ItemGroup01(Id),
		CONSTRAINT FK_Item_ItemGroup02Id FOREIGN KEY(ItemGroup02Id) REFERENCES ItemGroup02(Id),
		CONSTRAINT FK_Item_ItemGroup03Id FOREIGN KEY(ItemGroup03Id) REFERENCES ItemGroup03(Id),
		CONSTRAINT FK_Item_ItemGroup04Id FOREIGN KEY(ItemGroup04Id) REFERENCES ItemGroup04(Id),
		CONSTRAINT FK_Item_ItemGroup05Id FOREIGN KEY(ItemGroup05Id) REFERENCES ItemGroup05(Id),
		CONSTRAINT FK_Item_ItemGroup06Id FOREIGN KEY(ItemGroup06Id) REFERENCES ItemGroup06(Id),
		CONSTRAINT FK_Item_ItemGroup07Id FOREIGN KEY(ItemGroup07Id) REFERENCES ItemGroup07(Id),
		CONSTRAINT FK_Item_ItemGroup08Id FOREIGN KEY(ItemGroup08Id) REFERENCES ItemGroup08(Id),
		CONSTRAINT FK_Item_ItemGroup09Id FOREIGN KEY(ItemGroup09Id) REFERENCES ItemGroup09(Id),
		CONSTRAINT FK_Item_ItemGroup10Id FOREIGN KEY(ItemGroup10Id) REFERENCES ItemGroup10(Id),
		CONSTRAINT FK_Item_ItemGroup11Id FOREIGN KEY(ItemGroup11Id) REFERENCES ItemGroup11(Id),
		CONSTRAINT FK_Item_ItemGroup12Id FOREIGN KEY(ItemGroup12Id) REFERENCES ItemGroup12(Id),
		CONSTRAINT FK_Item_ItemGroup13Id FOREIGN KEY(ItemGroup13Id) REFERENCES ItemGroup13(Id),
		CONSTRAINT FK_Item_ItemGroup14Id FOREIGN KEY(ItemGroup14Id) REFERENCES ItemGroup14(Id),
		CONSTRAINT FK_Item_SaleTaxId FOREIGN KEY(SaleTaxId) REFERENCES Tax(Id),
		CONSTRAINT FK_Item_LinkedItemId FOREIGN KEY(LinkedItemId) REFERENCES Item(Id)
);

CREATE INDEX Item_RecId ON Item(RecId);

CREATE TABLE ItemPrices
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
		ItemId					INT NOT NULL,
		SaleTariffRegionId		VARCHAR(10) NOT NULL, 
		SizeRange				TINYINT NOT NULL,
		TradePrice				DECIMAL(9,2) DEFAULT 0,
		SalePrice				DECIMAL(9,2) DEFAULT 0,
		
		CONSTRAINT PK_ItemPrices PRIMARY KEY CLUSTERED(ItemId, SaleTariffRegionId, SizeRange),
		CONSTRAINT FK_ItemPrices_ItemId FOREIGN KEY(ItemId) REFERENCES Item(Id) ON DELETE CASCADE,
		CONSTRAINT FK_ItemPrices_SaleTariffRegionId FOREIGN KEY(SaleTariffRegionId) REFERENCES SaleTariffRegion(Id),
);	

CREATE INDEX ItemPrices_RecId ON ItemPrices(RecId);

CREATE TABLE ItemPhoto
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
		ItemId					INT NOT NULL,
		Number					INT NOT NULL,
		Bitmap					VARBINARY(MAX),
		
		CONSTRAINT [PK_ItemPhoto] PRIMARY KEY CLUSTERED(ItemId, Number),
		CONSTRAINT FK_ItemPhoto_ItemId FOREIGN KEY(ItemId) REFERENCES Item(Id) ON DELETE CASCADE
)

CREATE INDEX ItemPhoto_RecId ON ItemPhoto(RecId);

CREATE TABLE ItemBarcode
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
		Barcode					VARCHAR(20) NOT NULL,
		EntryType				SMALLINT NOT NULL, /* 0=EAN via Avelon's UI, 1=Imports */
		ItemId					INT NOT NULL,
		SizeIndex				SMALLINT NOT NULL,
		
		CONSTRAINT PK_ItemBarcode PRIMARY KEY CLUSTERED (ItemId, Barcode),
		CONSTRAINT FK_ItemBarcode_ItemId FOREIGN KEY(ItemId) REFERENCES Item(Id) ON DELETE CASCADE
);

CREATE INDEX ItemBarcode_RecId ON ItemBarcode(RecId);

CREATE TABLE SaleReturnReason
(
		RecId						INT IDENTITY(1,1),
		RecVersion					rowversion,
		Id							INT NOT NULL,
		Name						VARCHAR(64),
		Position					INT NOT NULL,	
		Visible						BIT NOT NULL,
		IsDefect					BIT NOT NULL,
		DefectShopId				INT,
	
		CONSTRAINT PK_SaleReturnReason PRIMARY KEY(Id)
);

CREATE INDEX SaleReturnReason_RecId ON SaleReturnReason(RecId);

CREATE TABLE SalePriceChangeReason
(
		RecId						INT IDENTITY(1,1),
		RecVersion					rowversion,
		Id							INT NOT NULL,
		Name						VARCHAR(64),
		Position					INT NOT NULL,	
		Visible						BIT NOT NULL,
	
		CONSTRAINT PK_SalePriceChangeReason PRIMARY KEY(Id)
);

CREATE INDEX SalePriceChangeReason_RecId ON SalePriceChangeReason(RecId);

CREATE TABLE IncomeAndExpensesReason
(
		RecId						INT IDENTITY(1,1),
		RecVersion					rowversion,
		Id							INT NOT NULL,
		Name						VARCHAR(64),
		ReasonType					INT NOT NULL,
		Position					INT NOT NULL,
		Visible						BIT NOT NULL,
	
		CONSTRAINT PK_IncomeAndExpensesReason PRIMARY KEY(Id)
);

CREATE INDEX IncomeAndExpensesReason_RecId ON IncomeAndExpensesReason(RecId);

CREATE TABLE SalePaymentMethod
(
		RecId						INT IDENTITY(1,1),
		RecVersion					rowversion,
		Id							VARCHAR(10) NOT NULL,
		Name						VARCHAR(64),
		CurrencyId					VARCHAR(10) NOT NULL,
		IsCash						BIT NOT NULL,
		IsCard						BIT NOT NULL,
		IsVoucherEmit				BIT NOT NULL,
		IsVoucherReceive			BIT NOT NULL,
		IsCredit					BIT NOT NULL,
		VoucherValidityCheck		BIT NOT NULL,
		OpenDrawer					BIT NOT NULL,
		AskExternalReference		BIT NOT NULL,
		IsRefundMethod				BIT NOT NULL,
		CcvIssuerCode				VARCHAR(2),
	
		CONSTRAINT PK_SalePaymentMethod PRIMARY KEY(Id),
		CONSTRAINT FK_SalePaymentMethod_CurrencyId FOREIGN KEY(CurrencyId) REFERENCES Currency(Id)
);

CREATE INDEX SalePaymentMethod_RecId ON SalePaymentMethod(RecId);

CREATE TABLE ShopSalePaymentMethod
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
		ShopId					INT NOT NULL,
		SalePaymentMethodId		VARCHAR(10) NOT NULL,
		Name					VARCHAR (32) NOT NULL,
		VisibleIndex			TINYINT NOT NULL,
		ShowAmountInEndOfDay	BIT NOT NULL,
		ShowInPaymentScreen		BIT NOT NULL,
		ShowInEndOfDayScreen	BIT NOT NULL,
		CcvAuthorization		BIT NOT NULL,

		CONSTRAINT PK_ShopSalePaymentMethod PRIMARY KEY CLUSTERED (ShopId, SalePaymentMethodId),
		CONSTRAINT FK_ShopSalePaymentMethod_ShopId FOREIGN KEY(ShopId) REFERENCES Shop(Id) ON DELETE CASCADE,
		CONSTRAINT FK_ShopSalePaymentMethod_SalePaymentMethodId FOREIGN KEY(SalePaymentMethodId) REFERENCES SalePaymentMethod(Id) ON DELETE CASCADE
);

CREATE INDEX ShopSalePaymentMethod_RecId ON ShopSalePaymentMethod(RecId);

CREATE TABLE RightGroup
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
		Id						INT NOT NULL,
		Description				VARCHAR(64),

		CONSTRAINT PK_RightGroup PRIMARY KEY(Id)
);

CREATE INDEX RightGroup_RecId ON RightGroup(RecId);

CREATE TABLE RightGroupDetail
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
		RightGroupId			INT NOT NULL,
		Id						VARCHAR(64) NOT NULL,
		Permission				INT NOT NULL,

		CONSTRAINT PK_RightGroupDetail PRIMARY KEY(RightGroupId, Id),
		CONSTRAINT FK_RightGroupDetail_RightGroupId FOREIGN KEY(RightGroupId) REFERENCES RightGroup(Id) ON DELETE CASCADE
);

CREATE INDEX RightGroupDetail_RecId ON RightGroupDetail(RecId);

CREATE TABLE Employee
(
		RecId									INT IDENTITY(1,1),
		RecVersion								rowversion,
		Id										INT NOT NULL,
		Name									VARCHAR(64),
		Surname1								VARCHAR(64),
		Surname2								VARCHAR(64),
		FiscalNumber							VARCHAR(20),
		Address1								VARCHAR(64),
		Address2								VARCHAR(64),
		ZipCode									VARCHAR(15),
		City									VARCHAR(32),
		Province								VARCHAR(32),
		CountryId								VARCHAR (10) NOT NULL,
		Phone1									VARCHAR(25),
		Phone2									VARCHAR(25),
		EMail									VARCHAR(64),
		Blocked									BIT DEFAULT 0,
		BackofficeAccess						BIT DEFAULT 0,
		BackofficePassword						VARCHAR(128),
		PosPin									VARCHAR(128),
		AllowFingerPrintEnrollment 				BIT,
		AllowFingerPrintEnrollmentUtcDateTime 	DATETIME,
		FingerPrintTemplate 					VARBINARY(MAX),
		ReportRights							XML,

		CONSTRAINT PK_Employee PRIMARY KEY CLUSTERED (Id),
		CONSTRAINT FK_Employee_CountryId FOREIGN KEY(CountryId) REFERENCES Country(Id)
);

CREATE INDEX Employee_RecId ON Employee(RecId);

CREATE TABLE EmployeeRightGroup
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
		EmployeeId				INT NOT NULL,
		RightGroupId			INT NOT NULL,

		CONSTRAINT PK_EmployeeRightGroup PRIMARY KEY CLUSTERED (EmployeeId, RightGroupId),
		CONSTRAINT FK_EmployeeRightGroup_EmployeeId FOREIGN KEY(EmployeeId) REFERENCES Employee(Id) ON DELETE CASCADE,
		CONSTRAINT FK_EmployeeRightGroup_RightGroupId FOREIGN KEY(RightGroupId) REFERENCES RightGroup(Id) ON DELETE CASCADE
)

CREATE INDEX EmployeeRightGroup_RecId ON EmployeeRightGroup(RecId);

GO

CREATE TABLE SaleSession
(
		ShopId						INT NOT NULL,
		TerminalId					INT NOT NULL,
	    Id							INT NOT NULL,
		SessionState				TINYINT NOT NULL, /* 0=Open, 1=Closed */
		OpenDate					DATETIME NOT NULL,
		OpenEmployeeId				INT NOT NULL,
		CloseDate					DATETIME,
		CloseEmployeeId				INT,
		CloseNote					TEXT,
		CreditNew					DECIMAL(9, 2) NOT NULL,
		CreditPaid					DECIMAL(9, 2) NOT NULL,
		GiftVoucherSold				DECIMAL(9, 2) NOT NULL,
		ReservationPrepayment		DECIMAL(9, 2) NOT NULL,
	    
		CONSTRAINT PK_SaleSession PRIMARY KEY (ShopId, TerminalId, Id)
);

CREATE TABLE SaleSessionDetail
(
		ShopId						INT NOT NULL,
		TerminalId					INT NOT NULL,
	    SessionId					INT NOT NULL,
	    SalePaymentMethodId			VARCHAR(10) NOT NULL,
		OpenAmount					DECIMAL(9, 2) NOT NULL,  -- May be an amount in foreign currency
		RegisteredAmount			DECIMAL(9, 2) NOT NULL,  -- May be an amount in foreign currency
		IntroducedAmount			DECIMAL(9, 2) NOT NULL,  -- May be an amount in foreign currency
		AmountLeft					DECIMAL(9, 2) NOT NULL,  -- May be an amount in foreign currency
		Payments					DECIMAL(9, 2) NOT NULL,  -- May be an amount in foreign currency
		Expenses					DECIMAL(9, 2) NOT NULL,  -- We only allow expenses in local curreny payment methods
		Income						DECIMAL(9, 2) NOT NULL,  -- We only allow income in local curreny payment methods
	    
		CONSTRAINT PK_SaleSessionDetail PRIMARY KEY (ShopId, TerminalId, SessionId, SalePaymentMethodId)
		-- TODO FK !!!!
);

CREATE TABLE SaleSessionIncomeAndExpenses
(
		ShopId						INT NOT NULL,
		TerminalId					INT NOT NULL,
		SessionId					INT NOT NULL,
	    SalePaymentMethodId			VARCHAR(10) NOT NULL,
	    Id							INT NOT NULL,
		EmployeeId					INT NOT NULL,
		EntryDate					DATETIME NOT NULL,
		EntryType					TINYINT NOT NULL, -- 0=Expenses, 1=Income
	    EntryDescription			VARCHAR(80) NOT NULL,
	    Amount						DECIMAL(9, 2) NOT NULL, -- We only allow expenses in local curreny payment methods
		IncomeAndExpensesReasonId	INT NOT NULL,
		
		CONSTRAINT PK_SaleSessionIncomeAndExpenses PRIMARY KEY (ShopId, TerminalId, Id),
		CONSTRAINT FK_SaleSessionIncomeAndExpenses_IncomeAndExpensesReasonId FOREIGN KEY(IncomeAndExpensesReasonId) REFERENCES IncomeAndExpensesReason(Id)
);

GO

CREATE TABLE SaleDocumentSeries
(
		ShopId					INT NOT NULL,
		TerminalId				INT NOT NULL,
		Id						VARCHAR(2) NOT NULL,
		LastUsedNumber			INT NOT NULL,

		CONSTRAINT PK_SaleDocumentSeries PRIMARY KEY(ShopId, TerminalId, Id)
);

CREATE TABLE SaleDocument
(
	    Id								INT IDENTITY(1,1),
	    OriginalId						INT NOT NULL,
		ShopId							INT NOT NULL,
		SessionId						INT NOT NULL,
		TerminalId						INT NOT NULL,
		CustomerId						BIGINT NOT NULL,
		EmployeeId						INT NOT NULL,
		TicketSeries					VARCHAR(2) NOT NULL,
		TicketNumber					INT NOT NULL,
		InvoiceSeries					VARCHAR(2),
		InvoiceNumber					INT,
		IsTicketConvertedIntoInvoice	BIT NOT NULL,
		IsGiftVoucherDocument			BIT NOT NULL,
		IsCreditPaymentDocument			BIT NOT NULL,
		CurrencyId						VARCHAR(10) NOT NULL,
		CurrencyRate					DECIMAL(9, 6) NOT NULL,
		Created							DATETIME NOT NULL,
		TotalDiscountOverpaymentValue 	DECIMAL(9, 2) NOT NULL,
		Total 							DECIMAL(9, 2) NOT NULL,
		BelgiumLastSealNumber 			VARCHAR(8),
		BelgiumCurrentSealNumber 		VARCHAR(8),
        OriginalSaleDocumentAsXml       TEXT,
        DeliveryRequestInfoAsXml        TEXT,
		
		CONSTRAINT PK_SaleDocument PRIMARY KEY(Id),
		CONSTRAINT FK_SaleDocument_ShopId FOREIGN KEY(ShopId) REFERENCES Shop(Id),
	    CONSTRAINT FK_SaleDocument_CustomerId FOREIGN KEY(CustomerId) REFERENCES Customer(Id),
	    CONSTRAINT FK_SaleDocument_CurrencyId FOREIGN KEY(CurrencyId) REFERENCES Currency(Id),
	    CONSTRAINT FK_SaleDocument_EmployeeId FOREIGN KEY(EmployeeId) REFERENCES Employee(Id)
        --CONSTRAINT FK_SaleDocument_Shop_Terminal_Session FOREIGN KEY(Shop, Terminal, Session) REFERENCES SaleSession(Shop, Terminal, Number),
);

CREATE INDEX SaleDocument_ShopId_TerminalId_OriginalId ON SaleDocument(ShopId, TerminalId, OriginalId);
CREATE INDEX SaleDocument_Created ON SaleDocument(Created) INCLUDE (Id,ShopId);

CREATE TABLE SaleDocumentLine
(
	    Id							INT IDENTITY(1,1),
		DocumentId					INT NOT NULL,
		EmployeeId					INT NOT NULL,
		LineType					TINYINT NOT NULL, /* 0=Item, 1=Unused, 2=GiftVoucher sold, 3=CreditPayment received, 4=Retouch sold, 5=Reservation prepayment received */
		ItemId						INT,
		Barcode						VARCHAR(20),
		SizeRange					TINYINT NOT NULL,
		SizeIndex					TINYINT NOT NULL,
		SizeName					VARCHAR(8) NOT NULL,
		Quantity					INT NOT NULL,
		Tariff			    		INT NOT NULL,
		OriginalPrice				DECIMAL(9, 2) NOT NULL,
		CurrentPrice				DECIMAL(9, 2) NOT NULL,
		DiscountLineDiscounts		DECIMAL(9, 2) NOT NULL,
		DiscountTotalDiscounts		DECIMAL(9, 2) NOT NULL,
		Discount					DECIMAL(9, 2) NOT NULL,
		SubtotalWoTotalDiscounts	DECIMAL(9, 2) NOT NULL,
		SubTotal					DECIMAL(9, 2) NOT NULL,
		TaxPercentage				DECIMAL(7, 3) NOT NULL,
		SalePriceChangeReasonId		INT NOT NULL, /* 0 in case OriginalPrice = CurrentPrice */
		IsReturned					BIT NOT NULL,
		ReturnReasonId				INT NOT NULL, /* 0 in case it is no return (Quantity > 0) */
		ReturnNote                  VARCHAR(256),
		ReturnShopId				INT NOT NULL,
		ReturnTerminalId			INT NOT NULL,
		ReturnSeries				VARCHAR(2),
		ReturnNumber				INT,
		ReturnLineId				INT,
		ItemEventId					INT,
		GiftVoucherId				VARCHAR(10),
    	CreditShopId				INT,
    	CreditTerminalId			INT,
    	CreditId					INT,
		CustomerOrderEntryId        INT,
		RetouchId					INT,
		RetouchN					INT,
		RetouchM					INT,
		PrepaymentOrderId			INT,
		PrepaymentVoucherShopId		INT,
		PrepaymentVoucherTerminalId INT,
		PrepaymentVoucherId			INT,

		CONSTRAINT PK_SaleDocumentLine PRIMARY KEY(Id),
        CONSTRAINT FK_SaleDocumentLine_DocumentId FOREIGN KEY(DocumentId) REFERENCES SaleDocument(Id),
        CONSTRAINT FK_SaleDocumentLine_ItemId FOREIGN KEY(ItemId) REFERENCES Item(Id),
        CONSTRAINT FK_SaleDocumentLine_SalePriceChangeReasonId FOREIGN KEY(SalePriceChangeReasonId) REFERENCES SalePriceChangeReason(Id),
        CONSTRAINT FK_SaleDocumentLine_ReturnReasonId FOREIGN KEY(ReturnReasonId) REFERENCES SaleReturnReason(Id)
);

CREATE INDEX SaleDocumentLine_DocumentId ON SaleDocumentLine(DocumentId);
CREATE INDEX SaleDocumentLine_Barcode ON SaleDocumentLine(Barcode);
CREATE INDEX SaleDocumentLine_LineType ON SaleDocumentLine(LineType) INCLUDE (DocumentId, Quantity, SubTotal);

CREATE TABLE SaleDocumentLineDiscount
(
		Id							INT IDENTITY(1,1),
		LineId						INT NOT NULL,
		DiscountReasonId			INT NOT NULL, -- Reference in the 'DiscountReason' table
		DiscountValue				DECIMAL(9, 2) NOT NULL, -- Value of the discount, e.j. 10% or 2€
		DiscountUnit				INT NOT NULL, -- 0=DiscountValue is abolute, 1=DiscountValue is a percentage
		DiscountAbsoluteValue		DECIMAL(9, 2) NOT NULL, -- Discount absolute value
		DiscountType				INT NOT NULL, -- 0=Line discount, 1=Pro-rata part of total discount
		TotalDiscountId				INT, -- Id of the total-discount that produced this line-discount
		IsPromotion					BIT NOT NULL, -- Indicate the discount was produced by a promotion
		EmployeeId					INT,
		VoucherBarcode				VARCHAR(30),
		VoucherShopId				INT,
		VoucherId					INT,
		ExternalRef 				VARCHAR(80), -- External reference entered by the user

		CONSTRAINT PK_SaleDocumentLineDiscount PRIMARY KEY(Id),
        CONSTRAINT FK_SaleDocumentLineDiscount_LineId FOREIGN KEY(LineId) REFERENCES SaleDocumentLine(Id),
	    CONSTRAINT FK_SaleDocumentLineDiscount_EmployeeId FOREIGN KEY(EmployeeId) REFERENCES Employee(Id)
);

CREATE INDEX SaleDocumentLineDiscount_LineId ON SaleDocumentLineDiscount(LineId);

CREATE TABLE SaleDocumentTotalDiscount
(
		Id							INT IDENTITY(1,1),
		DocumentId					INT NOT NULL,
		DiscountReasonId			INT NOT NULL, -- Reference in the 'DiscountReason' table
		DiscountValue				DECIMAL(9, 2) NOT NULL, -- Value of the discount, e.j. 10% or 2€
		DiscountUnit				INT NOT NULL, -- 0=DiscountValue is abolute, 1=DiscountValue is a percentage
		DiscountAbsoluteValue		DECIMAL(9, 2) NOT NULL, -- Discount absolute value
		IsLoyaltyDiscount			BIT NOT NULL,
		EmployeeId					INT,
		VoucherBarcode				VARCHAR(30),
		VoucherShopId				INT,
		VoucherTerminalId 			INT,
		VoucherId					INT,
		ExternalRef					VARCHAR(80),

		CONSTRAINT PK_SaleDocumentTotalDiscount PRIMARY KEY(Id),
        CONSTRAINT FK_SaleDocumentTotalDiscount_DocumentId FOREIGN KEY(DocumentId) REFERENCES SaleDocument(Id),
	    CONSTRAINT FK_SaleDocumentTotalDiscount_EmployeeId FOREIGN KEY(EmployeeId) REFERENCES Employee(Id)
);

CREATE INDEX SaleDocumentTotalDiscount_DocumentId ON SaleDocumentTotalDiscount(DocumentId);

CREATE TABLE SaleDocumentPayment
(
		Id					INT IDENTITY(1,1),
		DocumentId			INT NOT NULL,
		PaymentMethod		VARCHAR(10) NOT NULL,
    	Rate 				FLOAT NOT NULL,
		PaymentValue		DECIMAL(9, 2) NOT NULL,
		PaymentValueLocal	DECIMAL(9, 2) NOT NULL,
		VoucherShopId		INT,
		VoucherTerminalId	INT,
		VoucherId			INT,
   		CreditShopId		INT,
   		CreditTerminalId	INT,
   		CreditId			INT,
		ExternalReference	VARCHAR(200),

		CONSTRAINT PK_SaleDocumentPayment PRIMARY KEY(Id),
        CONSTRAINT FK_SaleDocumentPayment_PaymentMethodId FOREIGN KEY(PaymentMethod) REFERENCES SalePaymentMethod(Id),
        CONSTRAINT FK_SaleDocumentPayment_DocumentId FOREIGN KEY(DocumentId) REFERENCES SaleDocument(Id)
);

CREATE INDEX SaleDocumentPayment_DocumentId ON SaleDocumentPayment(DocumentId);

CREATE TABLE SaleDocumentInvoiceData
(
		ShopId						INT NOT NULL,
		TerminalId 					INT NOT NULL,
		DocumentId					INT NOT NULL,
		InvoiceSeries				NVARCHAR(2) NOT NULL,
		InvoiceNumber				INT NOT NULL,
		CustomerId					BIGINT, -- Only non null when a customer was selected
		Name						VARCHAR(64),
		FiscalNumber				VARCHAR(20),
		Address1					VARCHAR(64),
		Address2					VARCHAR(64),
		ZipCode						VARCHAR(15),
		City						VARCHAR(32),
		Province					VARCHAR(32),
		CountryId					VARCHAR (10) NOT NULL,

		CONSTRAINT PK_SaleDocumentInvoiceData PRIMARY KEY(ShopId, TerminalId, DocumentId)
);

GO

CREATE FUNCTION SizeRange(@SizeIndex INT, @SizeRange1 INT, @SizeRange2 INT, @SizeRange3 INT, @SizeRange4 INT, @SizeRange5 INT, @SizeRange6 INT, @SizeRange7 INT, @SizeRange8 INT)
	RETURNS INT
WITH EXECUTE AS CALLER
AS	
BEGIN
  if @SizeIndex <= @SizeRange1
		return 0;
  else if @SizeIndex > @SizeRange1 AND @SizeIndex <= @SizeRange2
		return 1;
  else if @SizeIndex > @SizeRange2 AND @SizeIndex <= @SizeRange3
		return 2;
  else if @SizeIndex > @SizeRange3 AND @SizeIndex <= @SizeRange4
		return 3;
  else if @SizeIndex > @SizeRange4 AND @SizeIndex <= @SizeRange5
		return 4;
  else if @SizeIndex > @SizeRange5 AND @SizeIndex <= @SizeRange6
		return 5;
  else if @SizeIndex > @SizeRange6 AND @SizeIndex <= @SizeRange7
		return 6;
  else if @SizeIndex > @SizeRange7 AND @SizeIndex <= @SizeRange8
		return 7;
	
  return 0;
END;

GO

CREATE TABLE ItemLabelPostfix
(
	ItemId					INT NOT NULL,
	SizeIndex				TINYINT NOT NULL,
	Postfix					INT NOT NULL DEFAULT 0,
		
	CONSTRAINT PK_ItemLabelPostfix PRIMARY KEY CLUSTERED(ItemId, SizeIndex),
    CONSTRAINT FK_ItemLabelPostfix_Item FOREIGN KEY(ItemId) REFERENCES Item(Id)
);

GO

CREATE PROCEDURE ReserveItemLabelPostfixes
   @ItemId INT,
   @SizeIndex TINYINT,
   @Quantity INT,
   @LowerBound INT OUTPUT,
   @UpperBound INT OUTPUT
AS
BEGIN
	if NOT EXISTS(SELECT * FROM ItemLabelPostfix WHERE ItemId = @ItemId AND SizeIndex = @SizeIndex)
		INSERT INTO ItemLabelPostfix (ItemId, SizeIndex, Postfix) VALUES(@ItemId, @SizeIndex, 0);
		
	DECLARE @LastUsedPostfix INT;
	SELECT @LastUsedPostfix = Postfix FROM ItemLabelPostfix WHERE ItemId = @ItemId AND SizeIndex = @SizeIndex;
	
	if @LastUsedPostfix + 1 + @Quantity > 9999 
	    SET @LastUsedPostfix = 0;
	    
	SET @LowerBound = @LastUsedPostfix + 1;
	SET @UpperBound = @LastUsedPostfix + @Quantity;
	
	UPDATE ItemLabelPostfix SET Postfix = @UpperBound WHERE ItemId = @ItemId AND SizeIndex = @SizeIndex;
END;

GO

CREATE FUNCTION VerhoeffCheckDigit(@Barcode VARCHAR(28)) 
	RETURNS CHAR
WITH EXECUTE AS CALLER
AS	
BEGIN
    -- http://en.wikipedia.org/wiki/Verhoeff_algorithm
    
	/* Declare the Arrays */
	DECLARE @d CHAR(100)
	DECLARE @p CHAR(80)
	DECLARE @inv CHAR(10)
	SET @d='0123456789123406789523401789563401289567401239567859876043216598710432765982104387659321049876543210'
	SET @p='01234567891576283094580379614289160435279453126870428657390127938064157046913258'
	SET @inv='0432156789'	

	DECLARE @ReverseBarcode VARCHAR(28);
	DECLARE @CheckDigit TINYINT
	DECLARE @len INT
	DECLARE @m TINYINT
	DECLARE @i SMALLINT
	DECLARE @Pos INT;
	DECLARE @Num INT;
	DECLARE @j INT;
	DECLARE @k INT;

	/* Start Processing */
	SET @CheckDigit = 0
	SET @ReverseBarcode = REVERSE(@Barcode);
	SET @len = Len(@ReverseBarcode);

	SET @i=0
	WHILE @i < @len
	BEGIN
		SET @Pos = ((@i + 1) % 8);
		SET @Num = CAST(SUBSTRING(@ReverseBarcode, @i + 1, 1) AS INT);
		SET @k = CAST(SUBSTRING(@p, (((@Pos * 10) + @Num) + 1), 1) AS INT);
		SET @j = ((@CheckDigit * 10) + @k);
		SET @CheckDigit = CAST(substring(@d, @j + 1, 1) AS TINYINT);
	
		SET @i=@i+1
	END

	SET @CheckDigit = CAST(substring(@inv,@CheckDigit + 1, 1) AS TINYINT)

	RETURN(@CheckDigit)
END;

GO

CREATE FUNCTION EncodeItemBarcode(@Item INT, @SizeIndex INT, @PostFix INT, @Shop INT)
	RETURNS VARCHAR(28)
WITH EXECUTE AS CALLER
AS	
BEGIN
    DECLARE @Result VARCHAR(20);
	SET @Result = RIGHT('00000000' + RTRIM(@Item), 8) + RIGHT('00' + RTRIM(@SizeIndex), 2) + RIGHT('000' + RTRIM(@Shop), 3) + RIGHT('0000' + RTRIM(@PostFix), 4);
	
	SELECT @Result = @Result + dbo.VerhoeffCheckDigit(@Result);
	RETURN @Result;
END;

GO

CREATE FUNCTION EncodeBarcode128C(@Barcode VARCHAR(28))
	RETURNS VARCHAR(28)
WITH EXECUTE AS CALLER
AS	
BEGIN
	-- Define the string of characters that we'll need to pull the reference of
	DECLARE @AsciiString VARCHAR(255)
	SET @AsciiString = CHAR(194) + '!"#$%&''()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~'
	SET @AsciiString = @AsciiString + CHAR(195) -- 0xC3
	SET @AsciiString = @AsciiString + CHAR(196) -- 0xC4
	SET @AsciiString = @AsciiString + CHAR(197) -- 0xC5
	SET @AsciiString = @AsciiString + CHAR(198) -- 0xC6
	SET @AsciiString = @AsciiString + CHAR(199) -- 0xC7
	SET @AsciiString = @AsciiString + CHAR(200) -- 0xC8
	SET @AsciiString = @AsciiString + CHAR(201) -- 0xC9
	SET @AsciiString = @AsciiString + CHAR(202) -- 0xCA
	
	-- Define the stop and start characters
	DECLARE @StopChar CHAR(1)
	DECLARE @StartChar CHAR(1)
	DECLARE @SpaceChar CHAR(1)
	
	SET @StopChar = CHAR(206) -- 0xCE
	SET @StartChar = CHAR(205) 
	SET @SpaceChar = CHAR(194) -- 0xC2

	-- Define the final holding place of our output string
	DECLARE @FinalArray VARCHAR(28)
	DECLARE @ChecksumTotal INT
	DECLARE @Checksum INT
	
	SET @ChecksumTotal = 105;
	SET @Checksum = 0;

	-- Loop through our input variable and start pulling out stuff
	DECLARE @Position INT
	DECLARE @Idx INT;
	DECLARE @DigitsPair CHAR(2)
	DECLARE @AsciiValue CHAR(1);
	
	SET @Position = 1
	SET @Idx = 1;
	SET @FinalArray = @StartChar;
	
	WHILE @Position <= LEN(@Barcode)
	BEGIN
		SET @DigitsPair = SUBSTRING(@Barcode, @Position, 2);
		SET @AsciiValue = SUBSTRING(@AsciiString, CAST(@DigitsPair AS INT) + 1, 1);
		SET @ChecksumTotal = @ChecksumTotal + (@Idx * CAST(@DigitsPair AS INT));
		SET @FinalArray = @FinalArray + @AsciiValue;
	
		SET @Position = @Position + 2
		SET @Idx = @Idx + 1;
	END

	SET @Checksum = @ChecksumTotal % 103;
	SET @FinalArray = @FinalArray + SUBSTRING(@AsciiString, @Checksum + 1, 1);
	SET @FinalArray = @FinalArray + @StopChar

    --Return the string
    return @FinalArray;
END;

GO

CREATE TABLE LabelLayout
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
		Id				        INT NOT NULL,
		Name			        VARCHAR(80),
		LabelType		        TINYINT DEFAULT 0,
		PrinterType		        TINYINT, -- 0=Graphic printer, 1=Dedicated printer
		FastReportFile          VARBINARY(MAX),
		DedicatedDefinition     VARCHAR(MAX),

		CONSTRAINT PK_LabelLayout PRIMARY KEY CLUSTERED (Id)
);

GO

CREATE TABLE LabelDefinition
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
		Id				        INT NOT NULL,
		Name			        VARCHAR(80),
		LayoutId		        INT NOT NULL,
		SortOrder		        INT DEFAULT 0,
		IsAvailableOnPos		BIT NOT NULL,
		OneLabelPerStyle 		BIT NOT NULL,

		CONSTRAINT PK_LabelDefinition PRIMARY KEY CLUSTERED (Id),
		CONSTRAINT FK_LabelDefinition_LabelLayout FOREIGN KEY(LayoutId) REFERENCES LabelLayout(Id)
);

GO

CREATE TABLE ReportLayout
(
		UserId			INT NOT NULL,
		ReportId		INT NOT NULL,
		ReportTitle		VARCHAR(64),
		Layout			XML,

		CONSTRAINT PK_ReportLayout PRIMARY KEY CLUSTERED (UserId, ReportId)
);

GO

CREATE TABLE ItemFeed
(
		Id				int identity(1,1) not null,
		CreateDate		datetime not null,
		Process			VARCHAR(80),
		DeleteFlag		bit not null default 0,
);

CREATE TABLE ItemFeedEntry
(
		ItemFeedId		int	not null,
		ItemId			int not null
);

CREATE INDEX ItemFeedEntry_ItemFeedId ON ItemFeedEntry(ItemFeedId) INCLUDE (ItemId);

GO

CREATE TABLE Album
(
	Id				INT IDENTITY(1,1),
	Name			VARCHAR(80),
	EmployeeId		INT NOT NULL,	

	CONSTRAINT PK_Album PRIMARY KEY CLUSTERED (Id),
	CONSTRAINT FK_Album_Employee FOREIGN KEY(EmployeeId) REFERENCES Employee(Id)
);

CREATE TABLE AlbumEntry
(
	Id				INT IDENTITY(1,1),
	AlbumId			INT NOT NULL,
	Name			VARCHAR(512),
	Bitmap			VARBINARY(MAX),
	Thumbnail		VARBINARY(MAX),

	CONSTRAINT PK_AlbumEntry PRIMARY KEY CLUSTERED (Id),
	CONSTRAINT FK_AlbumEntry_Album FOREIGN KEY(AlbumId) REFERENCES Album(Id) ON DELETE CASCADE
);

CREATE TABLE AlbumSharedWithEntry
(
	AlbumId			INT NOT NULL,
	EmployeeId		INT NOT NULL,

	CONSTRAINT PK_AlbumSharedWithEntry PRIMARY KEY CLUSTERED (AlbumId, EmployeeId),
	CONSTRAINT FK_AlbumSharedWithEntry_Album FOREIGN KEY(AlbumId) REFERENCES Album(Id) ON DELETE CASCADE,
	CONSTRAINT FK_AlbumSharedWithEntry_Employee FOREIGN KEY(EmployeeId) REFERENCES Employee(Id) ON DELETE CASCADE
);

CREATE TABLE ReportQueue
(
		Id						INT IDENTITY(1,1),
		ReportCulture			VARCHAR(5) NOT NULL,
		ReportId				VARCHAR(38) NOT NULL,
		ComputerName			VARCHAR(32),
		UserName				VARCHAR(256),
		ReportState				TINYINT NOT NULL, /* 0=Queued, 1=Preparing, 2=Prepared, 3=Sent, 4 = Error */
		ReportFeedback			VARCHAR(512),
		ReportPath				VARCHAR(512),
		QueueDateTime			DATETIME NOT NULL,
		PreparingDateTime		DATETIME,
		PreparedDateTime		DATETIME,
		SentDateTime			DATETIME,
		Recipients				VARCHAR(512),

		CONSTRAINT PK_ReportQueue PRIMARY KEY CLUSTERED (Id)
);

CREATE TABLE ReportQueueParameter
(
		Id						INT IDENTITY(1,1),
		QueueId					INT NOT NULL,
		ParameterType			VARCHAR(32) NOT NULL,
		Name					VARCHAR(128) NOT NULL,
		BitValue				BIT,
		IntValue				INT,
		StringValue				VARCHAR(MAX),
		DateValue				DATETIME,
		DateTimeValue			DATETIME,
		ByteArrayValue			VARBINARY(MAX),
		
		CONSTRAINT PK_ReportQueueParameters PRIMARY KEY CLUSTERED (Id),
		CONSTRAINT FK_ReportQueueParameter_QueueId FOREIGN KEY(QueueId) REFERENCES ReportQueue(Id) ON DELETE CASCADE
);

CREATE TABLE ReportQueueParameterListEntry
(
		Id						INT IDENTITY(1,1),
		QueueParameterId		INT NOT NULL,
		IntValue				INT,
		StringValue				VARCHAR(128),
		
		CONSTRAINT PK_ReportQueueParameterListEntry PRIMARY KEY CLUSTERED (Id),
		CONSTRAINT FK_ReportQueueParameterListEntry_QueueId FOREIGN KEY(QueueParameterId) REFERENCES ReportQueueParameter(Id) ON DELETE CASCADE
);

CREATE TABLE PurchaseOrderCancelReason
(
		RecId						INT IDENTITY(1,1),
		RecVersion					rowversion,
		Id							INT NOT NULL,
		Name						VARCHAR(64),
		Position					INT NOT NULL,	
		Visible						BIT NOT NULL,
	
		CONSTRAINT PK_PurchaseOrderCancelReason PRIMARY KEY(Id)
);

CREATE INDEX PurchaseOrderCancelReason_RecId ON PurchaseOrderCancelReason(RecId);

CREATE TABLE PosDocumentDefinition
(
		RecId						INT IDENTITY(1,1),
		RecVersion					rowversion,
		Culture						VARCHAR(16) NOT NULL,
		Id							INT NOT NULL,
		Name						VARCHAR(80),
		XmlDefinition				XML NOT NULL,
		
		CONSTRAINT PK_PosDocumentDefinition PRIMARY KEY(Culture, Id)
);

CREATE INDEX PosDocumentDefinition_RecId ON PosDocumentDefinition(RecId);

GO

CREATE FUNCTION QuantityRangeN
( 
	   @SizeRange   INT,
	   @Quantity00  INT,
	   @Quantity01  INT,
	   @Quantity02  INT,
	   @Quantity03  INT,
	   @Quantity04  INT,
	   @Quantity05  INT,
	   @Quantity06  INT,
	   @Quantity07  INT,
	   @Quantity08  INT,
	   @Quantity09  INT,
	   @Quantity10  INT,
	   @Quantity11  INT,
	   @Quantity12  INT,
	   @Quantity13  INT,
	   @Quantity14  INT,
	   @Quantity15  INT,
	   @Quantity16  INT,
	   @Quantity17  INT,
	   @Quantity18  INT,
	   @Quantity19  INT,
	   @Quantity20  INT,
	   @Quantity21  INT,
	   @Quantity22  INT,
	   @Quantity23  INT,
	   @Quantity24  INT,
	   @Quantity25  INT,
	   @SizeRanges	TINYINT,
	   @SizeSplit1	TINYINT,
	   @SizeSplit2	TINYINT,
	   @SizeSplit3	TINYINT,
	   @SizeSplit4	TINYINT,
	   @SizeSplit5	TINYINT,
	   @SizeSplit6	TINYINT,
	   @SizeSplit7	TINYINT,
	   @SizeSplit8	TINYINT
)
RETURNS INT
BEGIN 
  DECLARE @Quantity INT;
  SET @Quantity = 0;
 
  if (@SizeRange = 1)
  begin
	  if  0 <= @SizeSplit1 SET @Quantity = @Quantity + @Quantity00;
	  if  1 <= @SizeSplit1 SET @Quantity = @Quantity + @Quantity01;
	  if  2 <= @SizeSplit1 SET @Quantity = @Quantity + @Quantity02;
	  if  3 <= @SizeSplit1 SET @Quantity = @Quantity + @Quantity03;
	  if  4 <= @SizeSplit1 SET @Quantity = @Quantity + @Quantity04;
	  if  5 <= @SizeSplit1 SET @Quantity = @Quantity + @Quantity05;
	  if  6 <= @SizeSplit1 SET @Quantity = @Quantity + @Quantity06;
	  if  7 <= @SizeSplit1 SET @Quantity = @Quantity + @Quantity07;
	  if  8 <= @SizeSplit1 SET @Quantity = @Quantity + @Quantity08;
	  if  9 <= @SizeSplit1 SET @Quantity = @Quantity + @Quantity09;
	  if 10 <= @SizeSplit1 SET @Quantity = @Quantity + @Quantity10;
	  if 11 <= @SizeSplit1 SET @Quantity = @Quantity + @Quantity11;
	  if 12 <= @SizeSplit1 SET @Quantity = @Quantity + @Quantity12;
	  if 13 <= @SizeSplit1 SET @Quantity = @Quantity + @Quantity13;
	  if 14 <= @SizeSplit1 SET @Quantity = @Quantity + @Quantity14;
	  if 15 <= @SizeSplit1 SET @Quantity = @Quantity + @Quantity15;
	  if 16 <= @SizeSplit1 SET @Quantity = @Quantity + @Quantity16;
	  if 17 <= @SizeSplit1 SET @Quantity = @Quantity + @Quantity17;
	  if 18 <= @SizeSplit1 SET @Quantity = @Quantity + @Quantity18;
	  if 19 <= @SizeSplit1 SET @Quantity = @Quantity + @Quantity19;
	  if 20 <= @SizeSplit1 SET @Quantity = @Quantity + @Quantity20;
	  if 21 <= @SizeSplit1 SET @Quantity = @Quantity + @Quantity21;
	  if 22 <= @SizeSplit1 SET @Quantity = @Quantity + @Quantity22;
	  if 23 <= @SizeSplit1 SET @Quantity = @Quantity + @Quantity23;
	  if 24 <= @SizeSplit1 SET @Quantity = @Quantity + @Quantity24;
	  if 25 <= @SizeSplit1 SET @Quantity = @Quantity + @Quantity25;
  end
  else if (@SizeRange = 2 AND @SizeRanges >= 2)
  begin
	  if @SizeSplit1 <  0 AND @SizeSplit2 >=  0 SET @Quantity = @Quantity + @Quantity00;
	  if @SizeSplit1 <  1 AND @SizeSplit2 >=  1 SET @Quantity = @Quantity + @Quantity01;
	  if @SizeSplit1 <  2 AND @SizeSplit2 >=  2 SET @Quantity = @Quantity + @Quantity02;
	  if @SizeSplit1 <  3 AND @SizeSplit2 >=  3 SET @Quantity = @Quantity + @Quantity03;
	  if @SizeSplit1 <  4 AND @SizeSplit2 >=  4 SET @Quantity = @Quantity + @Quantity04;
	  if @SizeSplit1 <  5 AND @SizeSplit2 >=  5 SET @Quantity = @Quantity + @Quantity05;
	  if @SizeSplit1 <  6 AND @SizeSplit2 >=  6 SET @Quantity = @Quantity + @Quantity06;
	  if @SizeSplit1 <  7 AND @SizeSplit2 >=  7 SET @Quantity = @Quantity + @Quantity07;
	  if @SizeSplit1 <  8 AND @SizeSplit2 >=  8 SET @Quantity = @Quantity + @Quantity08;
	  if @SizeSplit1 <  9 AND @SizeSplit2 >=  9 SET @Quantity = @Quantity + @Quantity09;
	  if @SizeSplit1 < 10 AND @SizeSplit2 >= 10 SET @Quantity = @Quantity + @Quantity10;
	  if @SizeSplit1 < 11 AND @SizeSplit2 >= 11 SET @Quantity = @Quantity + @Quantity11;
	  if @SizeSplit1 < 12 AND @SizeSplit2 >= 12 SET @Quantity = @Quantity + @Quantity12;
	  if @SizeSplit1 < 13 AND @SizeSplit2 >= 13 SET @Quantity = @Quantity + @Quantity13;
	  if @SizeSplit1 < 14 AND @SizeSplit2 >= 14 SET @Quantity = @Quantity + @Quantity14;
	  if @SizeSplit1 < 15 AND @SizeSplit2 >= 15 SET @Quantity = @Quantity + @Quantity15;
	  if @SizeSplit1 < 16 AND @SizeSplit2 >= 16 SET @Quantity = @Quantity + @Quantity16;
	  if @SizeSplit1 < 17 AND @SizeSplit2 >= 17 SET @Quantity = @Quantity + @Quantity17;
	  if @SizeSplit1 < 18 AND @SizeSplit2 >= 18 SET @Quantity = @Quantity + @Quantity18;
	  if @SizeSplit1 < 19 AND @SizeSplit2 >= 19 SET @Quantity = @Quantity + @Quantity19;
	  if @SizeSplit1 < 20 AND @SizeSplit2 >= 20 SET @Quantity = @Quantity + @Quantity20;
	  if @SizeSplit1 < 21 AND @SizeSplit2 >= 21 SET @Quantity = @Quantity + @Quantity21;
	  if @SizeSplit1 < 22 AND @SizeSplit2 >= 22 SET @Quantity = @Quantity + @Quantity22;
	  if @SizeSplit1 < 23 AND @SizeSplit2 >= 23 SET @Quantity = @Quantity + @Quantity23;
	  if @SizeSplit1 < 24 AND @SizeSplit2 >= 24 SET @Quantity = @Quantity + @Quantity24;
	  if @SizeSplit1 < 25 AND @SizeSplit2 >= 25 SET @Quantity = @Quantity + @Quantity25;
  end
  else if (@SizeRange = 3 AND @SizeRanges >= 3)
  begin
	  if @SizeSplit2 <  0 AND @SizeSplit3 >=  0 SET @Quantity = @Quantity + @Quantity00;
	  if @SizeSplit2 <  1 AND @SizeSplit3 >=  1 SET @Quantity = @Quantity + @Quantity01;
	  if @SizeSplit2 <  2 AND @SizeSplit3 >=  2 SET @Quantity = @Quantity + @Quantity02;
	  if @SizeSplit2 <  3 AND @SizeSplit3 >=  3 SET @Quantity = @Quantity + @Quantity03;
	  if @SizeSplit2 <  4 AND @SizeSplit3 >=  4 SET @Quantity = @Quantity + @Quantity04;
	  if @SizeSplit2 <  5 AND @SizeSplit3 >=  5 SET @Quantity = @Quantity + @Quantity05;
	  if @SizeSplit2 <  6 AND @SizeSplit3 >=  6 SET @Quantity = @Quantity + @Quantity06;
	  if @SizeSplit2 <  7 AND @SizeSplit3 >=  7 SET @Quantity = @Quantity + @Quantity07;
	  if @SizeSplit2 <  8 AND @SizeSplit3 >=  8 SET @Quantity = @Quantity + @Quantity08;
	  if @SizeSplit2 <  9 AND @SizeSplit3 >=  9 SET @Quantity = @Quantity + @Quantity09;
	  if @SizeSplit2 < 10 AND @SizeSplit3 >= 10 SET @Quantity = @Quantity + @Quantity10;
	  if @SizeSplit2 < 11 AND @SizeSplit3 >= 11 SET @Quantity = @Quantity + @Quantity11;
	  if @SizeSplit2 < 12 AND @SizeSplit3 >= 12 SET @Quantity = @Quantity + @Quantity12;
	  if @SizeSplit2 < 13 AND @SizeSplit3 >= 13 SET @Quantity = @Quantity + @Quantity13;
	  if @SizeSplit2 < 14 AND @SizeSplit3 >= 14 SET @Quantity = @Quantity + @Quantity14;
	  if @SizeSplit2 < 15 AND @SizeSplit3 >= 15 SET @Quantity = @Quantity + @Quantity15;
	  if @SizeSplit2 < 16 AND @SizeSplit3 >= 16 SET @Quantity = @Quantity + @Quantity16;
	  if @SizeSplit2 < 17 AND @SizeSplit3 >= 17 SET @Quantity = @Quantity + @Quantity17;
	  if @SizeSplit2 < 18 AND @SizeSplit3 >= 18 SET @Quantity = @Quantity + @Quantity18;
	  if @SizeSplit2 < 19 AND @SizeSplit3 >= 19 SET @Quantity = @Quantity + @Quantity19;
	  if @SizeSplit2 < 20 AND @SizeSplit3 >= 20 SET @Quantity = @Quantity + @Quantity20;
	  if @SizeSplit2 < 21 AND @SizeSplit3 >= 21 SET @Quantity = @Quantity + @Quantity21;
	  if @SizeSplit2 < 22 AND @SizeSplit3 >= 22 SET @Quantity = @Quantity + @Quantity22;
	  if @SizeSplit2 < 23 AND @SizeSplit3 >= 23 SET @Quantity = @Quantity + @Quantity23;
	  if @SizeSplit2 < 24 AND @SizeSplit3 >= 24 SET @Quantity = @Quantity + @Quantity24;
	  if @SizeSplit2 < 25 AND @SizeSplit3 >= 25 SET @Quantity = @Quantity + @Quantity25;
  end
  else if (@SizeRange = 4 AND @SizeRanges >= 4)
  begin
	  if @SizeSplit3 <  0 AND @SizeSplit4 >=  0 SET @Quantity = @Quantity + @Quantity00;
	  if @SizeSplit3 <  1 AND @SizeSplit4 >=  1 SET @Quantity = @Quantity + @Quantity01;
	  if @SizeSplit3 <  2 AND @SizeSplit4 >=  2 SET @Quantity = @Quantity + @Quantity02;
	  if @SizeSplit3 <  3 AND @SizeSplit4 >=  3 SET @Quantity = @Quantity + @Quantity03;
	  if @SizeSplit3 <  4 AND @SizeSplit4 >=  4 SET @Quantity = @Quantity + @Quantity04;
	  if @SizeSplit3 <  5 AND @SizeSplit4 >=  5 SET @Quantity = @Quantity + @Quantity05;
	  if @SizeSplit3 <  6 AND @SizeSplit4 >=  6 SET @Quantity = @Quantity + @Quantity06;
	  if @SizeSplit3 <  7 AND @SizeSplit4 >=  7 SET @Quantity = @Quantity + @Quantity07;
	  if @SizeSplit3 <  8 AND @SizeSplit4 >=  8 SET @Quantity = @Quantity + @Quantity08;
	  if @SizeSplit3 <  9 AND @SizeSplit4 >=  9 SET @Quantity = @Quantity + @Quantity09;
	  if @SizeSplit3 < 10 AND @SizeSplit4 >= 10 SET @Quantity = @Quantity + @Quantity10;
	  if @SizeSplit3 < 11 AND @SizeSplit4 >= 11 SET @Quantity = @Quantity + @Quantity11;
	  if @SizeSplit3 < 12 AND @SizeSplit4 >= 12 SET @Quantity = @Quantity + @Quantity12;
	  if @SizeSplit3 < 13 AND @SizeSplit4 >= 13 SET @Quantity = @Quantity + @Quantity13;
	  if @SizeSplit3 < 14 AND @SizeSplit4 >= 14 SET @Quantity = @Quantity + @Quantity14;
	  if @SizeSplit3 < 15 AND @SizeSplit4 >= 15 SET @Quantity = @Quantity + @Quantity15;
	  if @SizeSplit3 < 16 AND @SizeSplit4 >= 16 SET @Quantity = @Quantity + @Quantity16;
	  if @SizeSplit3 < 17 AND @SizeSplit4 >= 17 SET @Quantity = @Quantity + @Quantity17;
	  if @SizeSplit3 < 18 AND @SizeSplit4 >= 18 SET @Quantity = @Quantity + @Quantity18;
	  if @SizeSplit3 < 19 AND @SizeSplit4 >= 19 SET @Quantity = @Quantity + @Quantity19;
	  if @SizeSplit3 < 20 AND @SizeSplit4 >= 20 SET @Quantity = @Quantity + @Quantity20;
	  if @SizeSplit3 < 21 AND @SizeSplit4 >= 21 SET @Quantity = @Quantity + @Quantity21;
	  if @SizeSplit3 < 22 AND @SizeSplit4 >= 22 SET @Quantity = @Quantity + @Quantity22;
	  if @SizeSplit3 < 23 AND @SizeSplit4 >= 23 SET @Quantity = @Quantity + @Quantity23;
	  if @SizeSplit3 < 24 AND @SizeSplit4 >= 24 SET @Quantity = @Quantity + @Quantity24;
	  if @SizeSplit3 < 25 AND @SizeSplit4 >= 25 SET @Quantity = @Quantity + @Quantity25;
  end
  else if (@SizeRange = 5 AND @SizeRanges >= 5)
  begin
	  if @SizeSplit4 <  0 AND @SizeSplit5 >=  0 SET @Quantity = @Quantity + @Quantity00;
	  if @SizeSplit4 <  1 AND @SizeSplit5 >=  1 SET @Quantity = @Quantity + @Quantity01;
	  if @SizeSplit4 <  2 AND @SizeSplit5 >=  2 SET @Quantity = @Quantity + @Quantity02;
	  if @SizeSplit4 <  3 AND @SizeSplit5 >=  3 SET @Quantity = @Quantity + @Quantity03;
	  if @SizeSplit4 <  4 AND @SizeSplit5 >=  4 SET @Quantity = @Quantity + @Quantity04;
	  if @SizeSplit4 <  5 AND @SizeSplit5 >=  5 SET @Quantity = @Quantity + @Quantity05;
	  if @SizeSplit4 <  6 AND @SizeSplit5 >=  6 SET @Quantity = @Quantity + @Quantity06;
	  if @SizeSplit4 <  7 AND @SizeSplit5 >=  7 SET @Quantity = @Quantity + @Quantity07;
	  if @SizeSplit4 <  8 AND @SizeSplit5 >=  8 SET @Quantity = @Quantity + @Quantity08;
	  if @SizeSplit4 <  9 AND @SizeSplit5 >=  9 SET @Quantity = @Quantity + @Quantity09;
	  if @SizeSplit4 < 10 AND @SizeSplit5 >= 10 SET @Quantity = @Quantity + @Quantity10;
	  if @SizeSplit4 < 11 AND @SizeSplit5 >= 11 SET @Quantity = @Quantity + @Quantity11;
	  if @SizeSplit4 < 12 AND @SizeSplit5 >= 12 SET @Quantity = @Quantity + @Quantity12;
	  if @SizeSplit4 < 13 AND @SizeSplit5 >= 13 SET @Quantity = @Quantity + @Quantity13;
	  if @SizeSplit4 < 14 AND @SizeSplit5 >= 14 SET @Quantity = @Quantity + @Quantity14;
	  if @SizeSplit4 < 15 AND @SizeSplit5 >= 15 SET @Quantity = @Quantity + @Quantity15;
	  if @SizeSplit4 < 16 AND @SizeSplit5 >= 16 SET @Quantity = @Quantity + @Quantity16;
	  if @SizeSplit4 < 17 AND @SizeSplit5 >= 17 SET @Quantity = @Quantity + @Quantity17;
	  if @SizeSplit4 < 18 AND @SizeSplit5 >= 18 SET @Quantity = @Quantity + @Quantity18;
	  if @SizeSplit4 < 19 AND @SizeSplit5 >= 19 SET @Quantity = @Quantity + @Quantity19;
	  if @SizeSplit4 < 20 AND @SizeSplit5 >= 20 SET @Quantity = @Quantity + @Quantity20;
	  if @SizeSplit4 < 21 AND @SizeSplit5 >= 21 SET @Quantity = @Quantity + @Quantity21;
	  if @SizeSplit4 < 22 AND @SizeSplit5 >= 22 SET @Quantity = @Quantity + @Quantity22;
	  if @SizeSplit4 < 23 AND @SizeSplit5 >= 23 SET @Quantity = @Quantity + @Quantity23;
	  if @SizeSplit4 < 24 AND @SizeSplit5 >= 24 SET @Quantity = @Quantity + @Quantity24;
	  if @SizeSplit4 < 25 AND @SizeSplit5 >= 25 SET @Quantity = @Quantity + @Quantity25;
  end
  else if (@SizeRange = 6 AND @SizeRanges >= 6)
  begin
	  if @SizeSplit5 <  0 AND @SizeSplit6 >=  0 SET @Quantity = @Quantity + @Quantity00;
	  if @SizeSplit5 <  1 AND @SizeSplit6 >=  1 SET @Quantity = @Quantity + @Quantity01;
	  if @SizeSplit5 <  2 AND @SizeSplit6 >=  2 SET @Quantity = @Quantity + @Quantity02;
	  if @SizeSplit5 <  3 AND @SizeSplit6 >=  3 SET @Quantity = @Quantity + @Quantity03;
	  if @SizeSplit5 <  4 AND @SizeSplit6 >=  4 SET @Quantity = @Quantity + @Quantity04;
	  if @SizeSplit5 <  5 AND @SizeSplit6 >=  5 SET @Quantity = @Quantity + @Quantity05;
	  if @SizeSplit5 <  6 AND @SizeSplit6 >=  6 SET @Quantity = @Quantity + @Quantity06;
	  if @SizeSplit5 <  7 AND @SizeSplit6 >=  7 SET @Quantity = @Quantity + @Quantity07;
	  if @SizeSplit5 <  8 AND @SizeSplit6 >=  8 SET @Quantity = @Quantity + @Quantity08;
	  if @SizeSplit5 <  9 AND @SizeSplit6 >=  9 SET @Quantity = @Quantity + @Quantity09;
	  if @SizeSplit5 < 10 AND @SizeSplit6 >= 10 SET @Quantity = @Quantity + @Quantity10;
	  if @SizeSplit5 < 11 AND @SizeSplit6 >= 11 SET @Quantity = @Quantity + @Quantity11;
	  if @SizeSplit5 < 12 AND @SizeSplit6 >= 12 SET @Quantity = @Quantity + @Quantity12;
	  if @SizeSplit5 < 13 AND @SizeSplit6 >= 13 SET @Quantity = @Quantity + @Quantity13;
	  if @SizeSplit5 < 14 AND @SizeSplit6 >= 14 SET @Quantity = @Quantity + @Quantity14;
	  if @SizeSplit5 < 15 AND @SizeSplit6 >= 15 SET @Quantity = @Quantity + @Quantity15;
	  if @SizeSplit5 < 16 AND @SizeSplit6 >= 16 SET @Quantity = @Quantity + @Quantity16;
	  if @SizeSplit5 < 17 AND @SizeSplit6 >= 17 SET @Quantity = @Quantity + @Quantity17;
	  if @SizeSplit5 < 18 AND @SizeSplit6 >= 18 SET @Quantity = @Quantity + @Quantity18;
	  if @SizeSplit5 < 19 AND @SizeSplit6 >= 19 SET @Quantity = @Quantity + @Quantity19;
	  if @SizeSplit5 < 20 AND @SizeSplit6 >= 20 SET @Quantity = @Quantity + @Quantity20;
	  if @SizeSplit5 < 21 AND @SizeSplit6 >= 21 SET @Quantity = @Quantity + @Quantity21;
	  if @SizeSplit5 < 22 AND @SizeSplit6 >= 22 SET @Quantity = @Quantity + @Quantity22;
	  if @SizeSplit5 < 23 AND @SizeSplit6 >= 23 SET @Quantity = @Quantity + @Quantity23;
	  if @SizeSplit5 < 24 AND @SizeSplit6 >= 24 SET @Quantity = @Quantity + @Quantity24;
	  if @SizeSplit5 < 25 AND @SizeSplit6 >= 25 SET @Quantity = @Quantity + @Quantity25;
  end
  else if (@SizeRange = 7 AND @SizeRanges >= 7)
  begin
	  if @SizeSplit6 <  0 AND @SizeSplit7 >=  0 SET @Quantity = @Quantity + @Quantity00;
	  if @SizeSplit6 <  1 AND @SizeSplit7 >=  1 SET @Quantity = @Quantity + @Quantity01;
	  if @SizeSplit6 <  2 AND @SizeSplit7 >=  2 SET @Quantity = @Quantity + @Quantity02;
	  if @SizeSplit6 <  3 AND @SizeSplit7 >=  3 SET @Quantity = @Quantity + @Quantity03;
	  if @SizeSplit6 <  4 AND @SizeSplit7 >=  4 SET @Quantity = @Quantity + @Quantity04;
	  if @SizeSplit6 <  5 AND @SizeSplit7 >=  5 SET @Quantity = @Quantity + @Quantity05;
	  if @SizeSplit6 <  6 AND @SizeSplit7 >=  6 SET @Quantity = @Quantity + @Quantity06;
	  if @SizeSplit6 <  7 AND @SizeSplit7 >=  7 SET @Quantity = @Quantity + @Quantity07;
	  if @SizeSplit6 <  8 AND @SizeSplit7 >=  8 SET @Quantity = @Quantity + @Quantity08;
	  if @SizeSplit6 <  9 AND @SizeSplit7 >=  9 SET @Quantity = @Quantity + @Quantity09;
	  if @SizeSplit6 < 10 AND @SizeSplit7 >= 10 SET @Quantity = @Quantity + @Quantity10;
	  if @SizeSplit6 < 11 AND @SizeSplit7 >= 11 SET @Quantity = @Quantity + @Quantity11;
	  if @SizeSplit6 < 12 AND @SizeSplit7 >= 12 SET @Quantity = @Quantity + @Quantity12;
	  if @SizeSplit6 < 13 AND @SizeSplit7 >= 13 SET @Quantity = @Quantity + @Quantity13;
	  if @SizeSplit6 < 14 AND @SizeSplit7 >= 14 SET @Quantity = @Quantity + @Quantity14;
	  if @SizeSplit6 < 15 AND @SizeSplit7 >= 15 SET @Quantity = @Quantity + @Quantity15;
	  if @SizeSplit6 < 16 AND @SizeSplit7 >= 16 SET @Quantity = @Quantity + @Quantity16;
	  if @SizeSplit6 < 17 AND @SizeSplit7 >= 17 SET @Quantity = @Quantity + @Quantity17;
	  if @SizeSplit6 < 18 AND @SizeSplit7 >= 18 SET @Quantity = @Quantity + @Quantity18;
	  if @SizeSplit6 < 19 AND @SizeSplit7 >= 19 SET @Quantity = @Quantity + @Quantity19;
	  if @SizeSplit6 < 20 AND @SizeSplit7 >= 20 SET @Quantity = @Quantity + @Quantity20;
	  if @SizeSplit6 < 21 AND @SizeSplit7 >= 21 SET @Quantity = @Quantity + @Quantity21;
	  if @SizeSplit6 < 22 AND @SizeSplit7 >= 22 SET @Quantity = @Quantity + @Quantity22;
	  if @SizeSplit6 < 23 AND @SizeSplit7 >= 23 SET @Quantity = @Quantity + @Quantity23;
	  if @SizeSplit6 < 24 AND @SizeSplit7 >= 24 SET @Quantity = @Quantity + @Quantity24;
	  if @SizeSplit6 < 25 AND @SizeSplit7 >= 25 SET @Quantity = @Quantity + @Quantity25;
  end
  else if (@SizeRange = 8 AND @SizeRanges >= 8)
  begin
	  if @SizeSplit7 <  0 AND @SizeSplit8 >=  0 SET @Quantity = @Quantity + @Quantity00;
	  if @SizeSplit7 <  1 AND @SizeSplit8 >=  1 SET @Quantity = @Quantity + @Quantity01;
	  if @SizeSplit7 <  2 AND @SizeSplit8 >=  2 SET @Quantity = @Quantity + @Quantity02;
	  if @SizeSplit7 <  3 AND @SizeSplit8 >=  3 SET @Quantity = @Quantity + @Quantity03;
	  if @SizeSplit7 <  4 AND @SizeSplit8 >=  4 SET @Quantity = @Quantity + @Quantity04;
	  if @SizeSplit7 <  5 AND @SizeSplit8 >=  5 SET @Quantity = @Quantity + @Quantity05;
	  if @SizeSplit7 <  6 AND @SizeSplit8 >=  6 SET @Quantity = @Quantity + @Quantity06;
	  if @SizeSplit7 <  7 AND @SizeSplit8 >=  7 SET @Quantity = @Quantity + @Quantity07;
	  if @SizeSplit7 <  8 AND @SizeSplit8 >=  8 SET @Quantity = @Quantity + @Quantity08;
	  if @SizeSplit7 <  9 AND @SizeSplit8 >=  9 SET @Quantity = @Quantity + @Quantity09;
	  if @SizeSplit7 < 10 AND @SizeSplit8 >= 10 SET @Quantity = @Quantity + @Quantity10;
	  if @SizeSplit7 < 11 AND @SizeSplit8 >= 11 SET @Quantity = @Quantity + @Quantity11;
	  if @SizeSplit7 < 12 AND @SizeSplit8 >= 12 SET @Quantity = @Quantity + @Quantity12;
	  if @SizeSplit7 < 13 AND @SizeSplit8 >= 13 SET @Quantity = @Quantity + @Quantity13;
	  if @SizeSplit7 < 14 AND @SizeSplit8 >= 14 SET @Quantity = @Quantity + @Quantity14;
	  if @SizeSplit7 < 15 AND @SizeSplit8 >= 15 SET @Quantity = @Quantity + @Quantity15;
	  if @SizeSplit7 < 16 AND @SizeSplit8 >= 16 SET @Quantity = @Quantity + @Quantity16;
	  if @SizeSplit7 < 17 AND @SizeSplit8 >= 17 SET @Quantity = @Quantity + @Quantity17;
	  if @SizeSplit7 < 18 AND @SizeSplit8 >= 18 SET @Quantity = @Quantity + @Quantity18;
	  if @SizeSplit7 < 19 AND @SizeSplit8 >= 19 SET @Quantity = @Quantity + @Quantity19;
	  if @SizeSplit7 < 20 AND @SizeSplit8 >= 20 SET @Quantity = @Quantity + @Quantity20;
	  if @SizeSplit7 < 21 AND @SizeSplit8 >= 21 SET @Quantity = @Quantity + @Quantity21;
	  if @SizeSplit7 < 22 AND @SizeSplit8 >= 22 SET @Quantity = @Quantity + @Quantity22;
	  if @SizeSplit7 < 23 AND @SizeSplit8 >= 23 SET @Quantity = @Quantity + @Quantity23;
	  if @SizeSplit7 < 24 AND @SizeSplit8 >= 24 SET @Quantity = @Quantity + @Quantity24;
	  if @SizeSplit7 < 25 AND @SizeSplit8 >= 25 SET @Quantity = @Quantity + @Quantity25;
  end

  RETURN @Quantity;
END;

GO

CREATE TABLE Prepack
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
    	Id						INT NOT NULL,
    	BrandId					INT NOT NULL,
  		SizeDomainId			VARCHAR(10) NOT NULL,
		SizeDomainConversionId	VARCHAR(10) NOT NULL,
		Name					VARCHAR(32) NOT NULL,
		QSize00					INT NOT NULL,
		QSize01					INT NOT NULL,
		QSize02					INT NOT NULL,
		QSize03					INT NOT NULL,
		QSize04					INT NOT NULL,
		QSize05					INT NOT NULL,
		QSize06					INT NOT NULL,
		QSize07					INT NOT NULL,
		QSize08					INT NOT NULL,
		QSize09					INT NOT NULL,
		QSize10					INT NOT NULL,
		QSize11					INT NOT NULL,
		QSize12					INT NOT NULL,
		QSize13					INT NOT NULL,
		QSize14					INT NOT NULL,
		QSize15					INT NOT NULL,
		QSize16					INT NOT NULL,
		QSize17					INT NOT NULL,
		QSize18					INT NOT NULL,
		QSize19					INT NOT NULL,
		QSize20					INT NOT NULL,
		QSize21					INT NOT NULL,
		QSize22					INT NOT NULL,
		QSize23					INT NOT NULL,
		QSize24					INT NOT NULL,
		QSize25					INT NOT NULL,
  	
		CONSTRAINT PK_Prepack PRIMARY KEY CLUSTERED (Id),
		CONSTRAINT FK_Prepack_BrandId FOREIGN KEY(BrandId) REFERENCES Brand(Id),
		CONSTRAINT FK_Prepack_SizeDomainId FOREIGN KEY(SizeDomainId) REFERENCES SizeDomain(Id),
		CONSTRAINT FK_Prepack_SizeDomainConversionId FOREIGN KEY(SizeDomainId, SizeDomainConversionId) REFERENCES SizeDomainConversion(SizeDomainId, Id)
);

CREATE INDEX Prepack_RecId ON Prepack(RecId);

GO

CREATE TABLE ItemPrepack
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
    	ItemId					INT NOT NULL,
    	PrepackId				INT NOT NULL,
    	    	
		CONSTRAINT PK_ItemPrepack PRIMARY KEY CLUSTERED (ItemId, PrepackId),
		CONSTRAINT FK_ItemPrepack_ItemId FOREIGN KEY(ItemId) REFERENCES Item(Id) ON DELETE CASCADE,
		CONSTRAINT FK_ItemPrepack_PrepackId FOREIGN KEY(PrepackId) REFERENCES Prepack(Id)
);

CREATE INDEX ItemPrepack_RecId ON ItemPrepack(RecId);

GO

CREATE TABLE ItemMinimum
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
		ItemId					INT NOT NULL,
    	ShopId					INT NOT NULL,
		Quantity00				INT NOT NULL,
		Quantity01				INT NOT NULL,
		Quantity02				INT NOT NULL,
		Quantity03				INT NOT NULL,
		Quantity04				INT NOT NULL,
		Quantity05				INT NOT NULL,
		Quantity06				INT NOT NULL,
		Quantity07				INT NOT NULL,
		Quantity08				INT NOT NULL,
		Quantity09				INT NOT NULL,
		Quantity10				INT NOT NULL,
		Quantity11				INT NOT NULL,
		Quantity12				INT NOT NULL,
		Quantity13				INT NOT NULL,
		Quantity14				INT NOT NULL,
		Quantity15				INT NOT NULL,
		Quantity16				INT NOT NULL,
		Quantity17				INT NOT NULL,
		Quantity18				INT NOT NULL,
		Quantity19				INT NOT NULL,
		Quantity20				INT NOT NULL,
		Quantity21				INT NOT NULL,
		Quantity22				INT NOT NULL,
		Quantity23				INT NOT NULL,
		Quantity24				INT NOT NULL,
		Quantity25				INT NOT NULL,
		
		CONSTRAINT PK_ItemMinimum PRIMARY KEY CLUSTERED (ItemId, ShopId),
		CONSTRAINT FK_ItemMinimum_ItemId FOREIGN KEY(ItemId) REFERENCES Item(Id) ON DELETE CASCADE,
		CONSTRAINT FK_ItemMinimum_ShopId FOREIGN KEY(ShopId) REFERENCES Shop(Id) ON DELETE CASCADE
);

GO

CREATE TABLE CatalogImportedItems
(
      ImportKey					VARCHAR(128),
      ItemId					INT,
      
	  CONSTRAINT FK_CatalogImportedItems_ItemId FOREIGN KEY(ItemId) REFERENCES Item(Id) ON DELETE CASCADE
);

CREATE INDEX CatalogImportedItems_ImportKey ON CatalogImportedItems(ImportKey);

GO

CREATE TABLE CatalogImportConfig
(
		Id						INT NOT NULL,
		Name					VARCHAR(32) NOT NULL,
		Config					VARCHAR(MAX) NOT NULL,
		Mapping					VARCHAR(MAX) NOT NULL,
		
		CONSTRAINT PK_CatalogImportTemplate PRIMARY KEY CLUSTERED (Id)
);

GO

CREATE TABLE ItemMinMax
(
		RecId					INT IDENTITY(1,1),
		RecVersion				rowversion,
		ItemId					INT NOT NULL,
    	ShopId					INT NOT NULL,
		MinQuantity00			INT NOT NULL,
		MinQuantity01			INT NOT NULL,
		MinQuantity02			INT NOT NULL,
		MinQuantity03			INT NOT NULL,
		MinQuantity04			INT NOT NULL,
		MinQuantity05			INT NOT NULL,
		MinQuantity06			INT NOT NULL,
		MinQuantity07			INT NOT NULL,
		MinQuantity08			INT NOT NULL,
		MinQuantity09			INT NOT NULL,
		MinQuantity10			INT NOT NULL,
		MinQuantity11			INT NOT NULL,
		MinQuantity12			INT NOT NULL,
		MinQuantity13			INT NOT NULL,
		MinQuantity14			INT NOT NULL,
		MinQuantity15			INT NOT NULL,
		MinQuantity16			INT NOT NULL,
		MinQuantity17			INT NOT NULL,
		MinQuantity18			INT NOT NULL,
		MinQuantity19			INT NOT NULL,
		MinQuantity20			INT NOT NULL,
		MinQuantity21			INT NOT NULL,
		MinQuantity22			INT NOT NULL,
		MinQuantity23			INT NOT NULL,
		MinQuantity24			INT NOT NULL,
		MinQuantity25			INT NOT NULL,
		MaxQuantity00			INT NOT NULL,
		MaxQuantity01			INT NOT NULL,
		MaxQuantity02			INT NOT NULL,
		MaxQuantity03			INT NOT NULL,
		MaxQuantity04			INT NOT NULL,
		MaxQuantity05			INT NOT NULL,
		MaxQuantity06			INT NOT NULL,
		MaxQuantity07			INT NOT NULL,
		MaxQuantity08			INT NOT NULL,
		MaxQuantity09			INT NOT NULL,
		MaxQuantity10			INT NOT NULL,
		MaxQuantity11			INT NOT NULL,
		MaxQuantity12			INT NOT NULL,
		MaxQuantity13			INT NOT NULL,
		MaxQuantity14			INT NOT NULL,
		MaxQuantity15			INT NOT NULL,
		MaxQuantity16			INT NOT NULL,
		MaxQuantity17			INT NOT NULL,
		MaxQuantity18			INT NOT NULL,
		MaxQuantity19			INT NOT NULL,
		MaxQuantity20			INT NOT NULL,
		MaxQuantity21			INT NOT NULL,
		MaxQuantity22			INT NOT NULL,
		MaxQuantity23			INT NOT NULL,
		MaxQuantity24			INT NOT NULL,
		MaxQuantity25			INT NOT NULL,
		
		CONSTRAINT PK_ItemMinMax PRIMARY KEY CLUSTERED (ItemId, ShopId),
		CONSTRAINT FK_ItemMinMax_ItemId FOREIGN KEY(ItemId) REFERENCES Item(Id) ON DELETE CASCADE,
		CONSTRAINT FK_ItemMinMax_ShopId FOREIGN KEY(ShopId) REFERENCES Shop(Id) ON DELETE CASCADE
);

GO

CREATE TABLE LoyaltyBalanceChange
(
		Id							INT IDENTITY(1,1),
		CustomerId					BIGINT NOT NULL,
		ChangeDateTime				DATETIME NOT NULL,
		Amount						DECIMAL(9, 2) NOT NULL,
		Reason						INT NOT NULL, /* 0=Import, 1=Save balance with a purchase, 2=Balance consumed, 3=Consume */
		SaleDocumentId				INT NOT NULL,
		AppliedSavingsPercentage	DECIMAL(5, 2) NOT NULL,

		CONSTRAINT PK_LoyaltyBalanceChange PRIMARY KEY CLUSTERED (Id)
);

CREATE INDEX LoyaltyBalanceChange_CustomerId ON LoyaltyBalanceChange(CustomerId);

GO

CREATE TABLE PrintServerQueue
(
		Id					INT IDENTITY(1, 1),
		QueueDateTime		DATETIME NOT NULL,
		ShopId				INT NOT NULL,
		TerminalId			INT NOT NULL,
		DocumentType		INT NOT NULL, /* 100=PackingSlip */
		DocumentFormat		INT NOT NULL, /* 0=Pdf, 1=Receipt, 2=Label */
		Int32Tag1			INT,
		Int32Tag2			INT,
		StringTag1			VARCHAR(1024) NOT NULL,
		StringTag2			VARCHAR(1024) NOT NULL,
		Printed				BIT NOT NULL,
		PrintedDateTime		DATETIME,

		CONSTRAINT PK_PrintServerQueueId PRIMARY KEY CLUSTERED (Id)
);

GO

CREATE TABLE SystemReportLayout
(
	Id				VARCHAR(36) NOT NULL,
	FrxReportFile	VARBINARY(MAX) NOT NULL,
	
	CONSTRAINT PK_SystemReportLayout PRIMARY KEY NONCLUSTERED(Id)
);

CREATE TABLE ReportDialogUserParameters
(
	UserId			INT NOT NULL,
	ReportId		VARCHAR(38) NOT NULL,
	Parameters		XML NOT NULL,

	CONSTRAINT PK_ReportDialogUserParameters PRIMARY KEY(UserId, ReportId)
);

GO

CREATE TABLE ItemGroupChangeHistory
(
	Id						INT IDENTITY(1,1),
	EmployeeId				INT NOT NULL,
	ModifiedDateTime		DATETIME NOT NULL,

	CONSTRAINT PK_ItemGroupChangeHistory PRIMARY KEY CLUSTERED (Id),
	CONSTRAINT FK_ItemGroupChangeHistory_EmployeeId FOREIGN KEY(EmployeeId) REFERENCES Employee(Id)
);

CREATE TABLE ItemGroupChangeHistoryEntry
(
	HistoryId			INT NOT NULL,
	ItemId				INT NOT NULL,
	GroupNumber			INT NOT NULL,
	GroupValue			VARCHAR(10) NOT NULL,

	CONSTRAINT PK_ItemGroupChangeHistoryEntry PRIMARY KEY CLUSTERED (HistoryId, ItemId, GroupNumber),
	CONSTRAINT FK_ItemGroupChangeHistory_HistoryId FOREIGN KEY(HistoryId) REFERENCES ItemGroupChangeHistory(Id)
);

GO

CREATE TABLE PosTariff
(
		RecId						INT IDENTITY(1,1),
		RecVersion					rowversion,
		Id							INT NOT NULL,
		Name						VARCHAR(255) NOT NULL,
		ActiveFrom					DATETIME NOT NULL,
		ActiveTill					DATETIME NOT NULL,
		DiscountReasonId			INT NOT NULL,
		
		CONSTRAINT PK_PosTariff PRIMARY KEY CLUSTERED (Id),
		CONSTRAINT FK_PosTariff_DiscountReasonId FOREIGN KEY(DiscountReasonId) REFERENCES DiscountReason(Id)
);

CREATE INDEX PosTariff_RecId ON PosTariff(RecId);
	
CREATE TABLE PosTariffShopPool
(
		RecId						INT IDENTITY(1,1),
		RecVersion					rowversion,
		PosTariffId					INT NOT NULL,
		ShopId					    INT NOT NULL,
		
		CONSTRAINT PK_PosTariffShopPool PRIMARY KEY CLUSTERED (PosTariffId, ShopId),
		CONSTRAINT FK_PosTariffShopPool_PosTariffId FOREIGN KEY(PosTariffId) REFERENCES PosTariff(Id) ON DELETE CASCADE,
		CONSTRAINT FK_PosTariffShopPool_ShopId FOREIGN KEY(ShopId) REFERENCES Shop(Id)
);

CREATE INDEX PosTariffShopPool_RecId ON PosTariffShopPool(RecId);

CREATE TABLE PosTariffItemPool
(
		RecId						INT IDENTITY(1,1),
		RecVersion					rowversion,
		PosTariffId					INT NOT NULL,
		ItemId					    INT NOT NULL,
		PriceRange1					DECIMAL(9, 2),
		PriceRange2					DECIMAL(9, 2),
		PriceRange3					DECIMAL(9, 2),
		PriceRange4					DECIMAL(9, 2),
		PriceRange5					DECIMAL(9, 2),
		PriceRange6					DECIMAL(9, 2),
		PriceRange7					DECIMAL(9, 2),
		PriceRange8					DECIMAL(9, 2),
		
		CONSTRAINT PK_PosTariffItemPool PRIMARY KEY CLUSTERED (PosTariffId, ItemId),
		CONSTRAINT FK_PosTariffItemPool_PosTariffId FOREIGN KEY(PosTariffId) REFERENCES PosTariff(Id) ON DELETE CASCADE,
		CONSTRAINT FK_PosTariffItemPool_ItemId FOREIGN KEY(ItemId) REFERENCES Item(Id)
);

CREATE INDEX PosTariffItemPool_RecId ON PosTariffItemPool(RecId);

GO

CREATE TABLE ItemLink
(
	OriginItemId		INT NOT NULL,
	DestinationItemId	INT NOT NULL,
	
	CONSTRAINT PK_ItemLink PRIMARY KEY CLUSTERED (OriginItemId, DestinationItemId),
	CONSTRAINT FK_ItemLink_OriginItemId FOREIGN KEY(OriginItemId) REFERENCES Item(Id),
	CONSTRAINT FK_ItemLink_DestinationItemId FOREIGN KEY(DestinationItemId) REFERENCES Item(Id)
);

GO

CREATE FUNCTION dbo.SplitItemQuantitiesBySizeRange(@Id INT, @SizeRanges INT, @SizeSplit1 INT, @SizeSplit2 INT, @SizeSplit3 INT, @SizeSplit4 INT, @SizeSplit5 int, @SizeSplit6 INT, @SizeSplit7 INT, @SizeSplit8 INT,
                                                   @Quantity00 INT, @Quantity01 INT, @Quantity02 INT, @Quantity03 INT, @Quantity04 INT, @Quantity05 INT, @Quantity06 INT, @Quantity07 INT, @Quantity08 INT, @Quantity09 INT,
												   @Quantity10 INT, @Quantity11 INT, @Quantity12 INT, @Quantity13 INT, @Quantity14 INT, @Quantity15 INT, @Quantity16 INT, @Quantity17 INT, @Quantity18 INT, @Quantity19 INT,
												   @Quantity20 INT, @Quantity21 INT, @Quantity22 INT, @Quantity23 INT, @Quantity24 INT, @Quantity25 INT)
RETURNS @TempTable TABLE 
(
	Id			INT NOT NULL,
	SizeRange	INT NOT NULL,
	Quantity00	INT NOT NULL,
	Quantity01	INT NOT NULL,
	Quantity02	INT NOT NULL,
	Quantity03	INT NOT NULL,
	Quantity04	INT NOT NULL,
	Quantity05	INT NOT NULL,
	Quantity06	INT NOT NULL,
	Quantity07	INT NOT NULL,
	Quantity08	INT NOT NULL,
	Quantity09	INT NOT NULL,
	Quantity10	INT NOT NULL,
	Quantity11	INT NOT NULL,
	Quantity12	INT NOT NULL,
	Quantity13	INT NOT NULL,
	Quantity14	INT NOT NULL,
	Quantity15	INT NOT NULL,
	Quantity16	INT NOT NULL,
	Quantity17	INT NOT NULL,
	Quantity18	INT NOT NULL,
	Quantity19	INT NOT NULL,
	Quantity20	INT NOT NULL,
	Quantity21	INT NOT NULL,
	Quantity22	INT NOT NULL,
	Quantity23	INT NOT NULL,
	Quantity24	INT NOT NULL,
	Quantity25	INT NOT NULL
)       
AS      
BEGIN
    DECLARE @Range INT;
    DECLARE @LowerBound INT;
    DECLARE @UpperBound INT;
    DECLARE @Qty00 INT;
    DECLARE @Qty01 INT;
    DECLARE @Qty02 INT;
    DECLARE @Qty03 INT;
    DECLARE @Qty04 INT;
    DECLARE @Qty05 INT;
    DECLARE @Qty06 INT;
    DECLARE @Qty07 INT;
    DECLARE @Qty08 INT;
    DECLARE @Qty09 INT;
    DECLARE @Qty10 INT;
    DECLARE @Qty11 INT;
    DECLARE @Qty12 INT;
    DECLARE @Qty13 INT;
    DECLARE @Qty14 INT;
    DECLARE @Qty15 INT;
    DECLARE @Qty16 INT;
    DECLARE @Qty17 INT;
    DECLARE @Qty18 INT;
    DECLARE @Qty19 INT;
    DECLARE @Qty20 INT;
    DECLARE @Qty21 INT;
    DECLARE @Qty22 INT;
    DECLARE @Qty23 INT;
    DECLARE @Qty24 INT;
    DECLARE @Qty25 INT;

	SET @Range = 1;
	WHILE @Range <= @SizeRanges
	  BEGIN
	    IF @Range = 1 
		  BEGIN
		    SET @LowerBound = 0;
			SET @UpperBound = @SizeSplit1;
		  END;
	    ELSE IF @Range = 2
		  BEGIN
		    SET @LowerBound = @SizeSplit1 + 1;
			SET @UpperBound = @SizeSplit2;
		  END;
	    ELSE IF @Range = 3
		  BEGIN
		    SET @LowerBound = @SizeSplit2 + 1;
			SET @UpperBound = @SizeSplit3;
		  END;
	    ELSE IF @Range = 4
		  BEGIN
		    SET @LowerBound = @SizeSplit3 + 1;
			SET @UpperBound = @SizeSplit4;
		  END;
	    ELSE IF @Range = 5
		  BEGIN
		    SET @LowerBound = @SizeSplit4 + 1;
			SET @UpperBound = @SizeSplit5;
		  END;
	    ELSE IF @Range = 6
		  BEGIN
		    SET @LowerBound = @SizeSplit5 + 1;
			SET @UpperBound = @SizeSplit6;
		  END;
	    ELSE IF @Range = 7
		  BEGIN
		    SET @LowerBound = @SizeSplit6 + 1;
			SET @UpperBound = @SizeSplit7;
		  END;
	    ELSE IF @Range = 8
		  BEGIN
		    SET @LowerBound = @SizeSplit7 + 1;
			SET @UpperBound = 25;
		  END;

		SET @Qty00 = CASE WHEN @LowerBound <= 0 AND @UpperBound >= 0 THEN @Quantity00 ELSE 0 END;
		SET @Qty01 = CASE WHEN @LowerBound <= 1 AND @UpperBound >= 1 THEN @Quantity01 ELSE 0 END;
		SET @Qty02 = CASE WHEN @LowerBound <= 2 AND @UpperBound >= 2 THEN @Quantity02 ELSE 0 END;
		SET @Qty03 = CASE WHEN @LowerBound <= 3 AND @UpperBound >= 3 THEN @Quantity03 ELSE 0 END;
		SET @Qty04 = CASE WHEN @LowerBound <= 4 AND @UpperBound >= 4 THEN @Quantity04 ELSE 0 END;
		SET @Qty05 = CASE WHEN @LowerBound <= 5 AND @UpperBound >= 5 THEN @Quantity05 ELSE 0 END;
		SET @Qty06 = CASE WHEN @LowerBound <= 6 AND @UpperBound >= 6 THEN @Quantity06 ELSE 0 END;
		SET @Qty07 = CASE WHEN @LowerBound <= 7 AND @UpperBound >= 7 THEN @Quantity07 ELSE 0 END;
		SET @Qty08 = CASE WHEN @LowerBound <= 8 AND @UpperBound >= 8 THEN @Quantity08 ELSE 0 END;
		SET @Qty09 = CASE WHEN @LowerBound <= 9 AND @UpperBound >= 9 THEN @Quantity09 ELSE 0 END;
		SET @Qty10 = CASE WHEN @LowerBound <= 10 AND @UpperBound >= 10 THEN @Quantity10 ELSE 0 END;
		SET @Qty11 = CASE WHEN @LowerBound <= 11 AND @UpperBound >= 11 THEN @Quantity11 ELSE 0 END;
		SET @Qty12 = CASE WHEN @LowerBound <= 12 AND @UpperBound >= 12 THEN @Quantity12 ELSE 0 END;
		SET @Qty13 = CASE WHEN @LowerBound <= 13 AND @UpperBound >= 13 THEN @Quantity13 ELSE 0 END;
		SET @Qty14 = CASE WHEN @LowerBound <= 14 AND @UpperBound >= 14 THEN @Quantity14 ELSE 0 END;
		SET @Qty15 = CASE WHEN @LowerBound <= 15 AND @UpperBound >= 15 THEN @Quantity15 ELSE 0 END;
		SET @Qty16 = CASE WHEN @LowerBound <= 16 AND @UpperBound >= 16 THEN @Quantity16 ELSE 0 END;
		SET @Qty17 = CASE WHEN @LowerBound <= 17 AND @UpperBound >= 17 THEN @Quantity17 ELSE 0 END;
		SET @Qty18 = CASE WHEN @LowerBound <= 18 AND @UpperBound >= 18 THEN @Quantity18 ELSE 0 END;
		SET @Qty19 = CASE WHEN @LowerBound <= 19 AND @UpperBound >= 19 THEN @Quantity19 ELSE 0 END;
		SET @Qty20 = CASE WHEN @LowerBound <= 20 AND @UpperBound >= 20 THEN @Quantity20 ELSE 0 END;
		SET @Qty21 = CASE WHEN @LowerBound <= 21 AND @UpperBound >= 21 THEN @Quantity21 ELSE 0 END;
		SET @Qty22 = CASE WHEN @LowerBound <= 22 AND @UpperBound >= 22 THEN @Quantity22 ELSE 0 END;
		SET @Qty23 = CASE WHEN @LowerBound <= 23 AND @UpperBound >= 23 THEN @Quantity23 ELSE 0 END;
		SET @Qty24 = CASE WHEN @LowerBound <= 24 AND @UpperBound >= 24 THEN @Quantity24 ELSE 0 END;
		SET @Qty25 = CASE WHEN @LowerBound <= 25 AND @UpperBound >= 25 THEN @Quantity25 ELSE 0 END;

		if (@Qty00 <> 0 OR @Qty01 <> 0 OR @Qty02 <> 0 OR @Qty03 <> 0 OR @Qty04 <> 0 OR @Qty05 <> 0 OR @Qty06 <> 0 OR @Qty07 <> 0 OR @Qty08 <> 0 OR @Qty09 <> 0 OR 
            @Qty10 <> 0 OR @Qty11 <> 0 OR @Qty12 <> 0 OR @Qty13 <> 0 OR @Qty14 <> 0 OR @Qty15 <> 0 OR @Qty16 <> 0 OR @Qty17 <> 0 OR @Qty18 <> 0 OR @Qty19 <> 0 OR 
            @Qty20 <> 0 OR @Qty21 <> 0 OR @Qty22 <> 0 OR @Qty23 <> 0 OR @Qty24 <> 0 OR @Qty25 <> 0)
			INSERT INTO @TempTable VALUES(@Id, @Range, @Qty00, @Qty01, @Qty02, @Qty03, @Qty04, @Qty05, @Qty06, @Qty07, @Qty08, @Qty09, @Qty10, @Qty11, @Qty12, @Qty13, @Qty14, @Qty15, @Qty16, @Qty17, @Qty18, @Qty19, @Qty20, @Qty21, @Qty22, @Qty23, @Qty24, @Qty25);

		SET @Range = @Range + 1;
	  END;

    RETURN
END;

GO

CREATE FUNCTION ISOweek (@DATE datetime)
RETURNS int
AS
BEGIN
   DECLARE @ISOweek INT;

   SET @ISOweek= DATEPART(wk, @DATE) + 1 - DATEPART(wk, CAST(DATEPART(yy, @DATE) as CHAR(4)) + '0104')

    --Special cases: Jan 1-3 may belong to the previous year
   IF (@ISOweek = 0)
      SET @ISOweek = dbo.ISOweek(CAST(DATEPART(yy,@DATE) - 1 AS CHAR(4)) + '12' + CAST(24 + DATEPART(DAY, @DATE) AS CHAR(2))) + 1

   --Special case: Dec 29-31 may belong to the next year
   IF ((DATEPART(mm, @DATE) = 12) AND ((DATEPART(dd, @DATE) - DATEPART(dw, @DATE)) >= 28))
      SET @ISOweek = 1;

   RETURN(@ISOweek)
END

GO

CREATE FUNCTION ISOyear (@DATE datetime)
RETURNS int
AS
BEGIN
   DECLARE @ISOweek INT;
   DECLARE @ISOyear INT;

   SET @ISOyear = DATEPART(yy, @DATE);
   SET @ISOweek = DATEPART(wk, @DATE) + 1 - DATEPART(wk, CAST(DATEPART(yy, @DATE) as CHAR(4)) + '0104')

    --Special cases: Jan 1-3 may belong to the previous year
   IF (@ISOweek = 0)
      SET @ISOyear = @ISOyear - 1;

   --Special case: Dec 29-31 may belong to the next year
   IF ((DATEPART(mm, @DATE) = 12) AND ((DATEPART(dd, @DATE) - DATEPART(dw, @DATE)) >= 28))
      SET @ISOyear = @ISOyear + 1;

   RETURN(@ISOyear)
END

GO

CREATE FUNCTION ActivePrices(@SaleTariffRegion VARCHAR(10))
RETURNS @Result TABLE
(
	ItemId INT,
	OriginalPrice1 DECIMAL(9,2),
	OriginalPrice2 DECIMAL(9,2),
	OriginalPrice3 DECIMAL(9,2),
	OriginalPrice4 DECIMAL(9,2),
	OriginalPrice5 DECIMAL(9,2),
	OriginalPrice6 DECIMAL(9,2),
	OriginalPrice7 DECIMAL(9,2),
	OriginalPrice8 DECIMAL(9,2),
	ActivePrice1 DECIMAL(9,2),
	ActivePrice2 DECIMAL(9,2),
	ActivePrice3 DECIMAL(9,2),
	ActivePrice4 DECIMAL(9,2),
	ActivePrice5 DECIMAL(9,2),
	ActivePrice6 DECIMAL(9,2),
	ActivePrice7 DECIMAL(9,2),
	ActivePrice8 DECIMAL(9,2)
) AS
BEGIN
 WITH Summary1(ItemId, RowNumber, PriceRange1, PriceRange2, PriceRange3, PriceRange4, PriceRange5, PriceRange6, PriceRange7, PriceRange8) 
 AS
 (
  SELECT p.ItemId, row_number() OVER (PARTITION BY ItemId ORDER BY t.ActiveFrom DESC) AS RowNumber, p.PriceRange1, p.PriceRange2, p.PriceRange3, p.PriceRange4, p.PriceRange5, p.PriceRange6, p.PriceRange7, p.PriceRange8
  FROM PosTariff t, PosTariffItemPool p
  WHERE t.Id = p.PosTariffId AND
     t.ActiveFrom <= GETDATE() AND
     t.ActiveTill >= GETDATE() AND
     t.Id IN (SELECT PosTariffId 
        FROM PosTariffShopPool p, Shop s
        WHERE p.ShopId = s.Id AND
        s.SaleTariffRegionId = @SaleTariffRegion)
 ),
 Summary2(ItemId, PriceRange1, PriceRange2, PriceRange3, PriceRange4, PriceRange5, PriceRange6, PriceRange7, PriceRange8)
 AS
 (
  SELECT ItemId, s.PriceRange1, s.PriceRange2, s.PriceRange3, s.PriceRange4, s.PriceRange5, s.PriceRange6, s.PriceRange7, s.PriceRange8
  FROM Summary1 s
  WHERE RowNumber = 1 
 ),
 Summary3(ItemId, SalePrice1, SalePrice2, SalePrice3, SalePrice4, SalePrice5, SalePrice6, SalePrice7, SalePrice8)
 AS
 (
  SELECT ItemId, ISNULL([1], 0) AS SalePrice1, ISNULL([2], 0) AS SalePrice2, ISNULL([3], 0) AS SalePrice3, ISNULL([4], 0) AS SalePrice4, ISNULL([5], 0) AS SalePrice5, ISNULL([6], 0) AS SalePrice6, ISNULL([7], 0) AS SalePrice7, ISNULL([8], 0) AS SalePrice8
  FROM (SELECT ItemId, SizeRange, SalePrice FROM ItemPrices WHERE SaleTariffRegionId = @SaleTariffRegion) AS SourceTable
  PIVOT (MAX(SalePrice) FOR SizeRange IN ([1], [2], [3], [4], [5], [6], [7], [8])) AS PivotTable
 ),
 Summary4(ItemId, OriginalPrice1, OriginalPrice2, OriginalPrice3, OriginalPrice4, OriginalPrice5, OriginalPrice6, OriginalPrice7, OriginalPrice8, ActivePrice1, ActivePrice2, ActivePrice3, ActivePrice4, ActivePrice5, ActivePrice6, ActivePrice7, ActivePrice8)
 AS
 (
  SELECT i.*,
      CASE WHEN t.ItemId IS NULL THEN SalePrice1 ELSE PriceRange1 END ActivePrice1,
      CASE WHEN t.ItemId IS NULL THEN SalePrice2 ELSE PriceRange2 END ActivePrice2,
      CASE WHEN t.ItemId IS NULL THEN SalePrice3 ELSE PriceRange3 END ActivePrice3,
      CASE WHEN t.ItemId IS NULL THEN SalePrice4 ELSE PriceRange4 END ActivePrice4,
      CASE WHEN t.ItemId IS NULL THEN SalePrice5 ELSE PriceRange5 END ActivePrice5,
      CASE WHEN t.ItemId IS NULL THEN SalePrice6 ELSE PriceRange6 END ActivePrice6,
      CASE WHEN t.ItemId IS NULL THEN SalePrice7 ELSE PriceRange7 END ActivePrice7,
      CASE WHEN t.ItemId IS NULL THEN SalePrice8 ELSE PriceRange8 END ActivePrice8
  FROM Summary3 i LEFT JOIN Summary2 t ON (i.ItemId = t.ItemId)
 )

 INSERT INTO @Result SELECT ItemId, OriginalPrice1, OriginalPrice2, OriginalPrice3, OriginalPrice4, OriginalPrice5, OriginalPrice6, OriginalPrice7, OriginalPrice8, ActivePrice1, ActivePrice2, ActivePrice3, ActivePrice4, ActivePrice5, ActivePrice6, ActivePrice7, ActivePrice8 FROM Summary4;

 RETURN;
END

GO

CREATE TABLE LabelPool
(
	Id							INT IDENTITY(1,1),
	Name						VARCHAR(24) NOT NULL,
	LabelDefinitionId			INT NOT NULL,

	CONSTRAINT PK_LabelPool PRIMARY KEY CLUSTERED (Id),
	CONSTRAINT FK_LabelPool_LabelDefinitionId FOREIGN KEY(LabelDefinitionId) REFERENCES LabelDefinition(Id),
);

CREATE TABLE LabelPoolShop
(
	PoolId		INT NOT NULL,
	ShopId		INT NOT NULL,

	CONSTRAINT PK_LabelPoolShop PRIMARY KEY CLUSTERED (PoolId, ShopId),
	CONSTRAINT FK_LabelPoolShop_PoolId FOREIGN KEY(PoolId) REFERENCES LabelPool(Id),
	CONSTRAINT FK_LabelPoolShop_ShopId FOREIGN KEY(ShopId) REFERENCES Shop(Id),
);

CREATE TABLE LabelPoolItem
(
	PoolId		INT NOT NULL,
	ItemId		INT NOT NULL,

	CONSTRAINT PK_LabelPoolItem PRIMARY KEY CLUSTERED (PoolId, ItemId),
	CONSTRAINT FK_LabelPoolItem_PoolId FOREIGN KEY(PoolId) REFERENCES LabelPool(Id),
	CONSTRAINT FK_LabelPoolItem_ShopId FOREIGN KEY(ItemId) REFERENCES Item(Id),
);

GO

CREATE TABLE PresenceControl
(
	Id INT IDENTITY(1,1) NOT NULL,
	ShopId INT NOT NULL,
	EmployeeId INT NOT NULL,
	EntryDateTime DATETIME NOT NULL,
	ExitDateTime DATETIME,
	State SMALLINT NOT NULL,
	
	CONSTRAINT PK_PresenceControl PRIMARY KEY(Id)
);

GO

CREATE TABLE POTemplate
(
	Id INT NOT NULL,
	Name VARCHAR(50),
	Shops VARCHAR(MAX),

	CONSTRAINT PK_POTemplate PRIMARY KEY CLUSTERED (Id)
);

GO

CREATE FUNCTION IntSizeLabelFromIndex(@ItemId INT, @SizeIndex INT)
   RETURNS VARCHAR(10)
WITH EXECUTE AS CALLER
AS	
BEGIN
	DECLARE @SizeLabel VARCHAR(10);

	SELECT @SizeLabel = CASE 
	    WHEN @SizeIndex = 0 THEN sd.IntSizeLabel00 
	    WHEN @SizeIndex = 1 THEN sd.IntSizeLabel01 
	    WHEN @SizeIndex = 2 THEN sd.IntSizeLabel02 
	    WHEN @SizeIndex = 3 THEN sd.IntSizeLabel03 
	    WHEN @SizeIndex = 4 THEN sd.IntSizeLabel04 
	    WHEN @SizeIndex = 5 THEN sd.IntSizeLabel05 
	    WHEN @SizeIndex = 6 THEN sd.IntSizeLabel06 
	    WHEN @SizeIndex = 7 THEN sd.IntSizeLabel07 
	    WHEN @SizeIndex = 8 THEN sd.IntSizeLabel08 
	    WHEN @SizeIndex = 9 THEN sd.IntSizeLabel09
	    WHEN @SizeIndex = 10 THEN sd.IntSizeLabel10 
	    WHEN @SizeIndex = 11 THEN sd.IntSizeLabel11 
	    WHEN @SizeIndex = 12 THEN sd.IntSizeLabel12 
	    WHEN @SizeIndex = 13 THEN sd.IntSizeLabel13 
	    WHEN @SizeIndex = 14 THEN sd.IntSizeLabel14 
	    WHEN @SizeIndex = 15 THEN sd.IntSizeLabel15 
	    WHEN @SizeIndex = 16 THEN sd.IntSizeLabel16 
	    WHEN @SizeIndex = 17 THEN sd.IntSizeLabel17 
	    WHEN @SizeIndex = 18 THEN sd.IntSizeLabel18 
	    WHEN @SizeIndex = 19 THEN sd.IntSizeLabel19
	    WHEN @SizeIndex = 20 THEN sd.IntSizeLabel20 
	    WHEN @SizeIndex = 21 THEN sd.IntSizeLabel21 
	    WHEN @SizeIndex = 22 THEN sd.IntSizeLabel22 
	    WHEN @SizeIndex = 23 THEN sd.IntSizeLabel23 
	    WHEN @SizeIndex = 24 THEN sd.IntSizeLabel24 
	    WHEN @SizeIndex = 25 THEN sd.IntSizeLabel25 
		ELSE '' END
	FROM Item i, SizeDomain sd
	WHERE i.SizeDomainId = sd.Id AND
		  i.Id = @ItemId;

	return @SizeLabel;
END;

GO