using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FullStack.Core.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Airlines",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "100000, 1000"),
                    Name = table.Column<string>(maxLength: 240, nullable: false),
                    ICAO = table.Column<string>(maxLength: 4, nullable: true),
                    IATA = table.Column<string>(maxLength: 4, nullable: true),
                    Image = table.Column<string>(maxLength: 50, nullable: false),
                    Callsign = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airlines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Airports",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "100000, 100"),
                    Name = table.Column<string>(maxLength: 240, nullable: false),
                    City = table.Column<string>(maxLength: 180, nullable: false),
                    State = table.Column<string>(maxLength: 180, nullable: false),
                    ICAO = table.Column<string>(maxLength: 4, nullable: true),
                    IATA = table.Column<string>(maxLength: 4, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "dbo",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    AirlineId = table.Column<int>(nullable: false),
                    Flight = table.Column<string>(maxLength: 10, nullable: false),
                    Gate = table.Column<string>(maxLength: 10, nullable: false),
                    OriginId = table.Column<int>(nullable: false),
                    DestinationId = table.Column<int>(nullable: false),
                    Scheduled = table.Column<DateTime>(nullable: false),
                    Passenger = table.Column<string>(maxLength: 180, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Airlines_AirlineId",
                        column: x => x.AirlineId,
                        principalSchema: "dbo",
                        principalTable: "Airlines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "dbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Airports_DestinationId",
                        column: x => x.DestinationId,
                        principalSchema: "dbo",
                        principalTable: "Airports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Airports_OriginId",
                        column: x => x.OriginId,
                        principalSchema: "dbo",
                        principalTable: "Airports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Airlines",
                columns: new[] { "Id", "Callsign", "IATA", "ICAO", "Image", "Name" },
                values: new object[,]
                {
                    { 100000, "SUL AMÉRICA", "", "", "airline_001.png", "ASTA Linhas Aéreas" },
                    { 101000, "AZUL", "AZU", "AD", "airline_002.png", "Azul Brazilian Airlines" },
                    { 102000, "GOL", "GLO", "G3", "airline_003.png", "GOL Linhas Aéreas Inteligentes" },
                    { 103000, "MAP AIR", "PAM", "", "airline_004.png", "MAP Linhas Aéreas" },
                    { 104000, "PASSAREDO", "PTB", "P3", "airline_005.png", "Passaredo Linhas Aéreas" },
                    { 105000, "PIQUIATUBA", "", "", "airline_006.png", "Piquiatuba Transportes Aéreos" },
                    { 106000, "SIDERAL", "SID", "", "airline_007.png", "Sideral" },
                    { 107000, "TAM", "TAM", "JJ", "airline_008.png", "LATAM Airlines" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Airports",
                columns: new[] { "Id", "City", "IATA", "ICAO", "Name", "State" },
                values: new object[,]
                {
                    { 116100, "Porecatu", "", "SSPK", "Porecatu", "Paraná" },
                    { 116000, "Ponta Porã", "PMG", "SBPP", "Ponta Porã International", "Mato Grosso do Sul" },
                    { 115900, "Ponta Grossa", "PGZ", "SSZW", "Comte. Antonio Amilton Beraldo", "Paraná" },
                    { 115800, "Poços de Caldas", "POO", "SBPC", "Emb. Walther Moreira Salles", "Minas Gerais" },
                    { 115700, "Picos", "PCS", "SNPC", "Sen. Helvídio Nunes", "Piauí" },
                    { 115300, "Patos de Minas", "POJ", "SNPD", "Pedro Pereira dos Santos", "Minas Gerais" },
                    { 115500, "Pelotas", "PET", "SBPK", "João Simões Lopes Neto International", "Rio Grande do Sul" },
                    { 115400, "Paulo Afonso", "PAV", "SBUF", "Paulo Afonso", "Bahia" },
                    { 116200, "Porto Alegre", "POA", "SBPA", "Salgado Filho International", "Rio Grande do Sul" },
                    { 115200, "Pato Branco", "PTO", "SSPB", "Juvenal Loureiro Cardoso", "Paraná" },
                    { 115100, "Passo Fundo", "PFB", "SBPF", "Lauro Kurtz", "Rio Grande do Sul" },
                    { 115600, "Petrolina", "PNZ", "SBPL", "Sen. Nilo Coelho", "Pernambuco" },
                    { 116300, "Porto de Moz", "PTQ", "SNMZ", "Porto de Moz", "Pará" },
                    { 116700, "Porto Urucu (Coari)", "", "SBUY", "Porto Urucu", "Amazonas" },
                    { 116500, "Porto Seguro", "BPS", "SBPS", "Porto Seguro", "Bahia" },
                    { 116600, "Porto de Trombetas (Oriximiná)", "TMT", "SBTB", "Porto de Trombetas", "Pará" },
                    { 115000, "Parnaíba", "PHB", "SBPB", "Pref. Dr. João Silva Filho International", "Piauí" },
                    { 116800, "Porto Velho", "PVH", "SBPV", "Gov. Jorge Teixeira de Oliveira International", "Rondônia" },
                    { 116900, "Presidente Prudente", "PPB", "SBDN", "Presidente Prudente", "São Paulo" },
                    { 117000, "Recife", "REC", "SBRF", "Guararapes-Gilberto Freyre International", "Pernambuco" },
                    { 117100, "Redenção", "RDC", "SNDC", "Redenção", "Pará" },
                    { 117200, "Resende", "REZ", "SDRS", "Resende", "Rio de Janeiro" },
                    { 117300, "Ribeirão Preto", "RAO", "SBRP", "Dr. Leite Lopes", "São Paulo" },
                    { 117400, "Rio Branco", "RBR", "SBRB", "Plácido de Castro International", "Acre" },
                    { 117500, "Rio de Janeiro", "GIG", "SBGL", "Galeão-Antonio Carlos Jobim International", "Rio de Janeiro" },
                    { 117600, "Rio de Janeiro", "QJR", "SBJR", "Jacarepaguá-Roberto Marinho", "Rio de Janeiro" },
                    { 116400, "Porto Nacional", "PNB", "SBPN", "Porto Nacional", "Tocantins" },
                    { 114900, "Parintins", "PIN", "SWPI", "Júlio Belém", "Amazonas" },
                    { 114500, "Ourinhos", "OUS", "SDOU", "Jornalista Benedito Pimentel", "São Paulo" },
                    { 114700, "Paranaguá", "PNG", "SSPG", "Santos Dumont", "Paraná" },
                    { 112100, "Lins", "LIP", "SBLN", "Gov. Lucas Nogueira Garcez", "São Paulo" },
                    { 112200, "Londrina", "LDB", "SBLO", "Gov. José Richa", "Paraná" },
                    { 112300, "Lucas do Rio Verde", "LVR", "SILC", "Bom Futuro", "Mato Grosso" },
                    { 112400, "Macaé", "MEA", "SBME", "Benedito Lacerda", "Rio de Janeiro" },
                    { 112500, "Macapá", "MCP", "SBMQ", "Alberto Alcolumbre International", "Amapá" },
                    { 112600, "Maceió", "MCZ", "SBMO", "Zumbi dos Palmares International", "Alagoas" },
                    { 112700, "Manaus", "MAO", "SBEG", "Brig. Eduardo Gomes International", "Amazonas" },
                    { 112800, "Manicoré", "MNX", "SBMY", "Manicoré", "Amazonas" },
                    { 112900, "Marabá", "MAB", "SBMA", "João Correa da Rocha", "Pará" },
                    { 113000, "Maricá", "", "SDMC", "Maricá", "Rio de Janeiro" },
                    { 113100, "Marília", "MII", "SBML", "Frank Miloye Milenkowichi", "São Paulo" },
                    { 113200, "Maringá", "MGF", "SBMG", "Sílvio Name Júnior Regional", "Paraná" },
                    { 114800, "Paranavaí", "PVI", "SSPI", "Edu Chaves", "Paraná" },
                    { 113300, "Matupá", "MBK", "SWXM", "Orlando Villas-Bôas Regional", "Mato Grosso" },
                    { 113500, "Minaçu", "MQH", "SWIQ", "Minaçu", "Goiás" },
                    { 113600, "Monte Dourado (Almeirim)", "MEU", "SBMD", "Serra do Areão", "Pará" },
                    { 113700, "Montes Claros", "MOC", "SBMK", "Mário Ribeiro", "Minas Gerais" },
                    { 113800, "Mossoró", "MVF", "SBMS", "Gov. Dix-Sept Rosado", "Rio Grande do Norte" },
                    { 113900, "Natal / São Gonçalo do Amarante", "NAT", "SBSG", "Gov. Aluízio Alves International", "Rio Grande do Norte" },
                    { 114000, "Navegantes", "NVT", "SBNF", "Min. Victor Konder International", "Santa Catarina" },
                    { 114100, "Novo Progresso", "NPR", "SJNP", "Novo Progresso", "Pará" },
                    { 114200, "Oeiras", "", "SNOE", "Oeiras", "Piauí" },
                    { 114300, "Oriximiná", "ORX", "SNOX", "Oriximiná", "Pará" },
                    { 114400, "Ourilândia do Norte", "OIA", "SDOW", "Ourilândia do Norte", "Pará" },
                    { 117700, "Rio de Janeiro", "SDU", "SBRJ", "Santos Dumont", "Rio de Janeiro" },
                    { 114600, "Palmas", "PMW", "SBPJ", "Brig. Lysias Rodrigues", "Tocantins" },
                    { 113400, "Maués", "MBZ", "SWMW", "Maués", "Amazonas" },
                    { 117800, "Rio Grande", "RIG", "SJRG", "Rio Grande Regional", "Rio Grande do Sul" },
                    { 118200, "Santa Isabel do Rio Negro", "IRZ", "SWTP", "Tapuruquara", "Amazonas" },
                    { 118000, "Rondonópolis", "ROO", "SWRD", "Maestro Marinho Franco", "Mato Grosso" },
                    { 121100, "Sorriso", "SMT", "SBSO", "Adolino Bedin Regional", "Mato Grosso" },
                    { 121200, "Tabatinga", "TBT", "SBTT", "Tabatinga International", "Amazonas" },
                    { 121300, "Tangará da Serra", "TGQ", "SWTS", "Tangará da Serra", "Mato Grosso" },
                    { 121400, "Tarauacá", "TRQ", "SBTK", "José Galera dos Santos", "Acre" },
                    { 121500, "Tefé", "TFF", "SBTF", "Tefé", "Amazonas" },
                    { 121600, "Teixeira de Freitas", "TXF", "SNTF", "9 de maio", "Bahia" },
                    { 121700, "Telêmaco Borba", "TEC", "SBTL", "Telêmaco Borba", "Paraná" },
                    { 121800, "Teófilo Otoni", "TFL", "SNTO", "Teófilo Otoni", "Minas Gerais" },
                    { 121900, "Teresina", "THE", "SBTE", "Sen. Petrônio Portella", "Piauí" },
                    { 122000, "Toledo", "TOW", "SBTD", "Luiz dal Canalle Filho", "Paraná" },
                    { 122100, "Três Lagoas", "TJL", "SBTG", "Plínio Alarcom", "Mato Grosso do Sul" },
                    { 122200, "Tucuruí", "TUR", "SBTU", "Tucuruí", "Pará" },
                    { 121000, "Sorocaba", "SOD", "SDCO", "Bertram Luiz Leupolz", "São Paulo" },
                    { 122300, "Ubatuba", "UBT", "SDUB", "Gastão Madeira", "São Paulo" },
                    { 122500, "Uberlândia", "UDI", "SBUL", "Ten. Cel. Av. César Bombonato", "Minas Gerais" },
                    { 122600, "Umuarama", "UMU", "SSUM", "Orlando de Carvalho", "Paraná" },
                    { 122700, "Una", "UNA", "SBTC", "Una-Comandatuba", "Bahia" },
                    { 122800, "União da Vitória", "QVB", "SSUV", "José Cleto", "Paraná" },
                    { 122900, "Uruguaiana", "URG", "SBUG", "Ruben Berta International", "Rio Grande do Sul" },
                    { 123000, "Valença", "VAL", "SNVB", "Valença", "Bahia" },
                    { 123100, "Varginha", "VAG", "SBVG", "Maj. Brig. Trompowsky", "Minas Gerais" },
                    { 123200, "Videira", "VIA", "SSVI", "Ângelo Ponzoni", "Santa Catarina" },
                    { 123300, "Vilhena", "BVH", "SBVH", "Brig. Camarão", "Rondônia" },
                    { 123400, "Vitória", "VIX", "SBVT", "Eurico de Aguiar Salles Airport (Goiabeiras)", "Espírito Santo" },
                    { 123500, "Vitória da Conquista", "VDC", "SSVC", "Glauber Rocha", "Bahia" },
                    { 123600, "Xinguara", "XIG", "", "Xinguara Municipal", "Pará" },
                    { 122400, "Uberaba", "UBA", "SBUR", "Mário de Almeida Franco", "Minas Gerais" },
                    { 117900, "Rio Verde", "RVD", "SWLC", "Gal. Leite de Castro", "Goiás" },
                    { 120900, "Sinop", "OPS", "SWSI", "Presidente João Figueiredo", "Mato Grosso" },
                    { 120700, "São Pedro", "", "SDAE", "São Pedro", "São Paulo" },
                    { 118100, "Salvador da Bahia", "SSA", "SBSV", "Dep. Luís Eduardo Magalhães International Airport (2 de Julho)", "Bahia" },
                    { 112000, "Lençóis", "LEC", "SBLE", "Cel. Horácio de Mattos", "Bahia" },
                    { 118300, "Santa Maria", "RIA", "SBSM", "Santa Maria", "Rio Grande do Sul" },
                    { 118400, "Santa Rosa", "SRA", "SSZR", "Luís Alberto Lehr", "Rio Grande do Sul" },
                    { 118500, "Santa Terezinha", "STZ", "SWST", "Santa Terezinha", "Mato Grosso" },
                    { 118600, "Santana do Araguaia", "CMP", "SNKE", "Santana do Araguaia", "Pará" },
                    { 118700, "Santarém", "STM", "SBSN", "Maestro Wilson Fonseca", "Pará" },
                    { 118800, "Santiago", "", "SSST", "Santiago", "Rio Grande do Sul" },
                    { 118900, "Santo Ângelo", "GEL", "SBNM", "Sepé Tiaraju", "Rio Grande do Sul" },
                    { 119000, "Santo Antônio do Içá", "IPG", "SWII", "Ipiranga", "Amazonas" },
                    { 119100, "São Benedito", "QXY", "SWBE", "Valfrido Salmito de Almeida", "Ceará" },
                    { 119200, "São Borja", "QOJ", "SSSB", "São Borja", "Rio Grande do Sul" },
                    { 120800, "São Raimundo Nonato", "", "SWKQ", "Serra da Capivara", "Piauí" },
                    { 119300, "São Carlos", "QSC", "SDSC", "Mário Pereira Lopes", "São Paulo" },
                    { 119500, "São Félix do Xingu", "SXX", "SNFX", "São Félix do Xingu", "Pará" },
                    { 119600, "São Gabriel da Cachoeira", "SJL", "SBUA", "São Gabriel da Cachoeira", "Amazonas" },
                    { 119700, "São João del-Rei", "JDR", "SNJR", "Pref. Octávio de Almeida Neves", "Minas Gerais" },
                    { 119800, "São José do Rio Preto", "SJP", "SBSR", "Prof. Eribelto Manoel Reino", "São Paulo" },
                    { 119900, "São José dos Campos", "SJK", "SBSJ", "Prof. Urbano Ernesto Stumpf", "São Paulo" },
                    { 120000, "São Lourenço", "SSO", "SNLO", "Comte. Luiz Carlos de Oliveira", "Minas Gerais" },
                    { 120100, "São Luís", "SLZ", "SBSL", "Mal. Cunha Machado International Airport (Tirirical)", "Maranhão" },
                    { 120200, "São Miguel do Oeste", "SQX", "SSOE", "Hélio Wasum", "Santa Catarina" },
                    { 120300, "São Paulo", "MAE", "SBMT", "Campo de Marte", "São Paulo" },
                    { 120400, "São Paulo", "CGH", "SBSP", "Congonhas", "São Paulo" },
                    { 120500, "São Paulo / Guarulhos", "GRU", "SBGR", "Gov. André Franco Montoro International Airport (Cumbica)", "São Paulo" },
                    { 120600, "São Paulo de Olivença", "OLC", "SDCG", "Sen. Eunice Michiles", "Amazonas" },
                    { 119400, "São Félix do Araguaia", "SXO", "SWFX", "São Félix do Araguaia", "Mato Grosso" },
                    { 111900, "Lages", "LAJ", "SBLJ", "Antônio Correia Pinto de Macedo", "Santa Catarina" },
                    { 111500, "Juiz de Fora / Goianá", "IZA", "SBZM", "Pres. Itamar Franco Airport (Zona da Mata)", "Minas Gerais" },
                    { 111700, "Juruena", "JRN", "SWJU", "Juruena", "Mato Grosso" },
                    { 103000, "Belém", "BEL", "SBBE", "Val de Cans-Júlio Cezar Ribeiro International", "Pará" },
                    { 103100, "Belo Horizonte", "", "SBPR", "Carlos Prates", "Minas Gerais" },
                    { 103200, "Belo Horizonte", "PLU", "SBBH", "Pampulha-Carlos Drummond de Andrade", "Minas Gerais" },
                    { 103300, "Belo Horizonte / Confins", "CNF", "SBCF", "Tancredo Neves International", "Minas Gerais" },
                    { 103400, "Belo Jardim", "", "SNBJ", "Belo Jardim", "Pernambuco" },
                    { 103500, "Boa Vista", "BVB", "SBBV", "Atlas Brasil Cantanhede International", "Roraima" },
                    { 103600, "Bom Jesus da Lapa", "LAZ", "SBLP", "Bom Jesus da Lapa", "Bahia" },
                    { 103700, "Bonito", "BYO", "SBDB", "Bonito", "Mato Grosso do Sul" },
                    { 103800, "Borba", "RBB", "SWBR", "Borba", "Amazonas" },
                    { 103900, "Bragança Paulista", "BJP", "SDBP", "Arthur Siqueira", "São Paulo" },
                    { 104000, "Brasília", "BSB", "SBBR", "Pres. Juscelino Kubitschek International", "Federal District" },
                    { 104100, "Breves", "BVS", "SNVS", "Breves", "Pará" },
                    { 102900, "Belém", "", "SBJC", "Brig. Protásio de Oliveira Airport (Júlio César)", "Pará" },
                    { 104200, "Cabo Frio", "CFB", "SBCB", "Cabo Frio International", "Rio de Janeiro" },
                    { 104400, "Cachoeiro de Itapemirim", "CDI", "SNKI", "Raimundo de Andrade", "Espírito Santo" },
                    { 104500, "Cacoal", "OAL", "SSKW", "Capital do Café", "Rondônia" },
                    { 104600, "Caldas Novas / Rio Quente", "CLV", "SBCN", "Nelson Ribeiro Guimarães", "Goiás" },
                    { 104700, "Campina Grande", "CPV", "SBKG", "Pres. João Suassuna", "Paraíba" },
                    { 104800, "Campinas", "", "SDAM", "Campo dos Amarais", "São Paulo" },
                    { 104900, "Campinas", "VCP", "SBKP", "Viracopos International", "São Paulo" },
                    { 105000, "Campo Grande", "CGR", "SBCG", "Campo Grande International", "Mato Grosso do Sul" },
                    { 105100, "Campo Mourão", "CBW", "SSKM", "Cel. Geraldo Guias de Aquino", "Paraná" },
                    { 105200, "Campos dos Goytacazes", "CAW", "SBCP", "Bartolomeu Lysandro", "Rio de Janeiro" },
                    { 105300, "Carajás (Parauapebas)", "CKS", "SBCJ", "Carajás", "Pará" },
                    { 105400, "Carauari", "CAF", "SWCA", "Carauari", "Amazonas" },
                    { 105500, "Caravelas", "CRQ", "SBCV", "Caravelas", "Bahia" },
                    { 104300, "Caçador", "CFC", "SBCD", "Carlos Alberto da Costa Neves", "Santa Catarina" },
                    { 105600, "Carolina", "CLN", "SBCI", "Carolina", "Maranhão" },
                    { 102800, "Bauru / Arealva", "JTC", "SBAE", "Moussa Nakhl Tobias", "São Paulo" },
                    { 102600, "Barretos", "BAT", "SNBA", "Chafei Amsei", "São Paulo" },
                    { 100000, "Água Boa", "HPX", "SWHP", "Água Boa", "Mato Grosso" },
                    { 100100, "Alegrete", "ALQ", "SSLT", "Gaudêncio Machado Ramos", "Rio Grande do Sul" },
                    { 100200, "Almeirim", "GGF", "SNYA", "Almeirim", "Pará" },
                    { 100300, "Alta Floresta", "AFL", "SBAT", "Piloto Oswaldo Marques Dias", "Mato Grosso" },
                    { 100400, "Altamira", "ATM", "SBHT", "Altamira", "Pará" },
                    { 100500, "Apucarana", "APU", "SSAP", "Capt. João Busse", "Paraná" },
                    { 100600, "Aracaju", "AJU", "SBAR", "Santa Maria", "Sergipe" },
                    { 100700, "Aracati", "ARX", "SBAC", "Dragão do Mar", "Ceará" },
                    { 100800, "Araçatuba", "ARU", "SBAU", "Dario Guarita", "São Paulo" },
                    { 100900, "Araguaína", "AUX", "SWGN", "Araguaína", "Tocantins" },
                    { 101000, "Arapongas", "APX", "SSOG", "Alberto Bertelli", "Paraná" },
                    { 101100, "Arapoti", "AAG", "SSYA", "Avelino Vieira", "Paraná" },
                    { 102700, "Bauru", "BAU", "SBBU", "Comte. João Ribeiro de Barros", "São Paulo" },
                    { 101200, "Araraquara", "AQA", "SBAQ", "Bartolomeu de Gusmão", "São Paulo" },
                    { 101400, "Aripuanã", "AIR", "SWRP", "Aripuanã", "Mato Grosso" },
                    { 101500, "Armação dos Búzios", "BZC", "SBBZ", "Umberto Modiano", "Rio de Janeiro" },
                    { 101600, "Arraias", "AAI", "SWRA", "Arraias", "Tocantins" },
                    { 101700, "Assis", "AIF", "SNAX", "Marcelo Pires Halzhausen", "São Paulo" },
                    { 101800, "Avaré / Arandu", "QVP", "SDRR", "Comte. Luiz Gonzaga Luth", "São Paulo" },
                    { 101900, "Bagé", "BGX", "SBBG", "Comte. Gustavo Kraemer", "Rio Grande do Sul" },
                    { 102000, "Barcelos", "BAZ", "SWBC", "Barcelos", "Amazonas" },
                    { 102100, "Barra", "BQQ", "SNBX", "Barra", "Bahia" },
                    { 102200, "Barra do Garças", "BPG", "SBBW", "Barra do Garças", "Mato Grosso" },
                    { 102300, "Barreiras", "BRA", "SNBR", "Barreiras", "Bahia" },
                    { 102400, "Barreirinha", "", "SWBI", "Barreirinha", "Amazonas" },
                    { 102500, "Barreirinhas", "BRB", "SSRS", "Barreirinhas", "Maranhão" },
                    { 101300, "Araxá", "AAX", "SBAX", "Araxá", "Minas Gerais" },
                    { 105700, "Caruaru", "CAU", "SNRU", "Oscar Laranjeiras", "Pernambuco" },
                    { 105800, "Cascavel", "CAC", "SBCA", "Adalberto Mendes da Silva", "Paraná" },
                    { 105900, "Caxias do Sul", "CXJ", "SBCX", "Hugo Cantergiani Regional Airport (Campo dos Bugres)", "Rio Grande do Sul" },
                    { 109000, "Goiânia", "GYN", "SBGO", "Santa Genoveva", "Goiás" },
                    { 109100, "Governador Valadares", "GVR", "SBGV", "Cel. Altino Machado de Oliveira", "Minas Gerais" },
                    { 109200, "Guaíra", "GGJ", "SSGY", "Walter Martins de Oliveira", "Paraná" },
                    { 109300, "Guanambi", "GNM", "SNGI", "Guanambi", "Bahia" },
                    { 109400, "Guarapuava", "GPB", "SBGU", "Tancredo Thomas de Faria", "Paraná" },
                    { 109500, "Gurupi", "GRP", "SWGI", "Gurupi", "Tocantins" },
                    { 109600, "Humaitá", "HUW", "SWHT", "Francisco Correa da Cruz", "Amazonas" },
                    { 109700, "Ijuí", "IJU", "SSIJ", "João Batista Bos Filho", "Rio Grande do Sul" },
                    { 109800, "Ilhéus", "IOS", "SBIL", "Jorge Amado", "Bahia" },
                    { 109900, "Imperatriz", "IMP", "SBIZ", "Pref. Renato Moreira", "Maranhão" },
                    { 110000, "Ipatinga / Santana do Paraíso", "IPN", "SBIP", "Usiminas", "Minas Gerais" },
                    { 110100, "Itaituba", "ITB", "SBIH", "Itaituba", "Pará" },
                    { 108900, "Goiânia", "", "SWNV", "Aeródromo Nacional de Aviação", "Goiás" },
                    { 110200, "Itanhaém", "", "SDIM", "Antônio Ribeiro Nogueira Jr.", "São Paulo" },
                    { 110400, "Jaguaruna", "JJG", "SBJA", "Humberto Ghizzo Bortoluzzi Regional", "Santa Catarina" },
                    { 110500, "Januária", "JNA", "SNJN", "Januária", "Minas Gerais" },
                    { 110600, "Jijoca de Jericoacoara / Cruz", "JJD", "SBJE", "Comte. Ariston Pessoa Regional", "Ceará" },
                    { 110700, "Ji-Paraná", "JPR", "SBJI", "José Coleto", "Rondônia" },
                    { 110800, "Joaçaba", "JCB", "SSJA", "Santa Terezinha", "Santa Catarina" },
                    { 110900, "João Pessoa / Bayeux", "JPA", "SBJP", "Pres. Castro Pinto International", "Paraíba" },
                    { 111000, "Joinville", "JOI", "SBJV", "Lauro Carneiro de Loyola", "Santa Catarina" },
                    { 111100, "Juara", "JUA", "SIZX", "Inácio Luís do Nascimento", "Mato Grosso" },
                    { 111200, "Juazeiro do Norte", "JDO", "SBJU", "Orlando Bezerra de Menezes", "Ceará" },
                    { 111300, "Juína", "JIA", "SWJN", "Juína", "Mato Grosso" },
                    { 111400, "Juiz de Fora", "JDF", "SBJF", "Francisco Álvares de Assis", "Minas Gerais" },
                    { 111600, "Jundiaí", "", "SBJD", "Comte. Rodolfo Rolim Amaro", "São Paulo" },
                    { 110300, "Itumbiara", "ITR", "SBIT", "Francisco Vilela do Amaral", "Goiás" },
                    { 111800, "Lábrea", "LBR", "SWLB", "Lábrea", "Amazonas" },
                    { 108800, "Gavião Peixoto", "", "SBGP", "Gavião Peixoto Aerodrome", "São Paulo" },
                    { 108600, "Francisco Beltrão", "FBE", "SSFB", "Paulo Abdala", "Paraná" },
                    { 106000, "Chapecó", "XAP", "SBCH", "Serafin Enoss Bertaso", "Santa Catarina" },
                    { 106100, "Cianorte", "QCN", "SSCT", "Gastão de Mesquita Filho", "Paraná" },
                    { 106200, "Coari", "CIZ", "SWKO", "Coari", "Amazonas" },
                    { 106300, "Conceição do Araguaia", "CDJ", "SBAA", "Conceição do Araguaia", "Pará" },
                    { 106400, "Concórdia", "CCI", "SSCK", "Olavo Cecco Rigon", "Santa Catarina" },
                    { 106500, "Confresa", "CFO", "SWKF", "Confresa", "Mato Grosso" },
                    { 106600, "Cornélio Procópio", "CKO", "SSCP", "Francisco Lacerda Junior", "Paraná" },
                    { 106700, "Corumbá", "CMG", "SBCR", "Corumbá International", "Mato Grosso do Sul" },
                    { 106800, "Criciúma / Forquilhinha", "CCM", "SBCM", "Diomício Freitas", "Santa Catarina" },
                    { 106900, "Cruzeiro do Sul", "CZS", "SBCZ", "Cruzeiro do Sul International", "Acre" },
                    { 107000, "Cuiabá / Várzea Grande", "CGB", "SBCY", "Mal. Rondon International", "Mato Grosso" },
                    { 107100, "Curitiba", "BFH", "SBBI", "Bacacheri", "Paraná" },
                    { 108700, "Garanhuns", "QGP", "SNGN", "Garanhuns", "Pernambuco" },
                    { 107200, "Curitiba / São José dos Pinhais", "CWB", "SBCT", "Afonso Pena International", "Paraná" },
                    { 107400, "Divinópolis", "DIQ", "SNDV", "Brig. Cabral", "Minas Gerais" },
                    { 107500, "Dourados", "DOU", "SBDO", "Francisco de Matos Pereira", "Mato Grosso do Sul" },
                    { 107600, "Eirunepé", "ERN", "SWEI", "Amaury Feitosa Tomaz", "Amazonas" },
                    { 107700, "Erechim", "ERM", "SSER", "Erechim", "Rio Grande do Sul" },
                    { 107800, "Feijó", "FEJ", "SNOU", "Feijó", "Acre" },
                    { 107900, "Feira de Santana", "FEC", "SBFE", "João Durval Carneiro", "Bahia" },
                    { 108000, "Fernando de Noronha", "FEN", "SBFN", "Fernando de Noronha", "Pernambuco" },
                    { 108100, "Florianópolis", "FLN", "SBFL", "Hercílio Luz International", "Santa Catarina" },
                    { 108200, "Fonte Boa", "FBA", "SWOB", "Fonte Boa", "Amazonas" },
                    { 108300, "Fortaleza", "FOR", "SBFZ", "Pinto Martins – Fortaleza International", "Ceará" },
                    { 108400, "Foz do Iguaçu", "IGU", "SBFI", "Cataratas International", "Paraná" },
                    { 108500, "Franca", "FRC", "SIMK", "Ten. Lund Presotto", "São Paulo" },
                    { 107300, "Diamantina", "DTI", "SNDT", "Juscelino Kubitschek", "Minas Gerais" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "10001", "00000000-0000-0000-0000-000000000000", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "00000000-0000-0000-0000-000000000000", 0, "00000000-0000-0000-0000-000000000000", "no-reply@test-fullstack.com.br", true, false, null, "NO-REPLY@TEST-FULLSTACK.COM.BR", "ADMIN", "AQAAAAEAACcQAAAAEOfSBBFEckky0kfQ1mDIRjM8vghRO4SfUBmlhtUjhgpWDAvKwNLHF2FAz0rzLqIy6w==", null, false, "00000000-0000-0000-0000-000000000000", false, "admin" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "00000000-0000-0000-0000-000000000000", "10001" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "dbo",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "dbo",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "dbo",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "dbo",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "dbo",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "dbo",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "dbo",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AirlineId",
                schema: "dbo",
                table: "Tickets",
                column: "AirlineId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CreatedBy",
                schema: "dbo",
                table: "Tickets",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_DestinationId",
                schema: "dbo",
                table: "Tickets",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_OriginId",
                schema: "dbo",
                table: "Tickets",
                column: "OriginId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Tickets",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Airlines",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Airports",
                schema: "dbo");
        }
    }
}
