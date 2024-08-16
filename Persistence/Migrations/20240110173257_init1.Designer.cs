﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence.EfData;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(EfBookStoreContext))]
    [Migration("20240110173257_init1")]
    partial class init1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Entities.CartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("Domain.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "Дэн Браун",
                            Description = "Роберт Лэнгдон прибывает в музей Гуггенхайма в Бильбао по приглашению друга и бывшего студента Эдмонда Кирша. Миллиардер и компьютерный гуру, он известен своими удивительными открытиями и предсказаниями. И этим вечером Кирш собирается «перевернуть все современные научные представления о мире», дав ответ на два главных вопроса, волнующих человечество на протяжении всей истории:Откуда мы? Что нас ждет?",
                            Genre = "Детектив",
                            Image = "https://cv9.litres.ru/pub/c/elektronnaya-kniga/cover_330/27624091-den-braun-proishozhdenie.jpg",
                            Price = 710m,
                            Rating = 3,
                            Title = "Происхождение"
                        },
                        new
                        {
                            Id = 2,
                            Author = "Джордж Оруэлл",
                            Description = "Своеобразный антипод второй великой антиутопии XX века – «О дивный новый мир» Олдоса Хаксли. Что, в сущности, страшнее: доведенное до абсурда «общество потребления» – или доведенное до абсолюта «общество идеи»? По Оруэллу, нет и не может быть ничего ужаснее тотальной несвободы…",
                            Genre = "Фантастика",
                            Image = "https://cv5.litres.ru/pub/c/elektronnaya-kniga/cover_330/50397852--.jpg",
                            Price = 415m,
                            Rating = 5,
                            Title = "1984"
                        },
                        new
                        {
                            Id = 3,
                            Author = "Донато Карризи",
                            Description = "Затерянный в Альпах сонный городок, рождественский вечер, туман. От дома, где сияют огни елки и лежат подарки, до празднично украшенной местной церкви всего триста метров, но в церкви юная Анна Лу так и не появилась… Вездесущие журналисты, фоторепортеры и телевизионщики осаждают городок. Каждый из них жаждет первым сообщить сенсационные новости о ходе расследования.",
                            Genre = "Детектив",
                            Image = "https://cv2.litres.ru/pub/c/elektronnaya-kniga/cover_330/27066425-donato-karrizi-devushka-v-tumane.jpg",
                            Price = 249m,
                            Rating = 4,
                            Title = "Девушка в тумане"
                        },
                        new
                        {
                            Id = 4,
                            Author = "Гузель Яхина",
                            Description = "Роман «Зулейха открывает глаза» начинается зимой 1930 года в глухой татарской деревне. Крестьянку Зулейху вместе с сотнями других переселенцев отправляют в вагоне-теплушке по извечному каторжному маршруту в Сибирь.Дремучие крестьяне и ленинградские интеллигенты, деклассированный элемент и уголовники, мусульмане и христиане, язычники и атеисты, русские, татары, немцы, чуваши – все встретятся на берегах Ангары, ежедневно отстаивая у тайги и безжалостного государства свое право на жизнь.",
                            Genre = "Современная проза",
                            Image = "https://cv8.litres.ru/pub/c/elektronnaya-kniga/cover_330/9527389-guzel-yahina-zuleyha-otkryvaet-glaza-9527389.jpg",
                            Price = 349m,
                            Rating = 5,
                            Title = "Зулейха открывает глаза"
                        },
                        new
                        {
                            Id = 5,
                            Author = "Джулия Эндерс",
                            Description = "Многие стесняются говорить о кишечнике вслух. Может быть, именно поэтому мы так мало знаем о самом могущественном органе, который управляет нашим организмом? Кого-то, возможно, шокирует столь откровенное обращение исследователя к «запретным» темам; кому-то, может быть, покажутся слишком экстремальными опыты на мышах и на пациентах-добровольцах, описанные в книге. Кто-то усомнится во всемогуществе крошечных организмов, контролирующих нашу жизнь. А кому-то предположение, что у кишечника есть свои собственные «мозг» и «нервная система», вообще покажется абсурдным и антинаучным.",
                            Genre = "Медицина и здоровье",
                            Image = "https://cv7.litres.ru/pub/c/elektronnaya-kniga/cover_extra/72060786-dzhuliya-enders-ocharovatelnyy-kishechnik-kak-samyy-moguschestvennyy-organ.gif",
                            Price = 415m,
                            Rating = 5,
                            Title = "Очаровательный кишечник"
                        },
                        new
                        {
                            Id = 6,
                            Author = "Виктор Пелевин",
                            Description = "Порфирий Петрович – литературно-полицейский алгоритм. Он расследует преступления и одновременно пишет об этом детективные романы, зарабатывая средства для Полицейского Управления.Маруха Чо – искусствовед с большими деньгами и баба с яйцами по официальному гендеру. Ее специальность – так называемый «гипс», искусство первой четверти XXI века. Ей нужен помощник для анализа рынка. Им становится взятый в аренду Порфирий.«iPhuck 10» – самый дорогой любовный гаджет на рынке и одновременно самый знаменитый из 244 детективов Порфирия Петровича. Это настоящий шедевр алгоритмической полицейской прозы конца века – энциклопедический роман о будущем любви, искусства и всего остального.",
                            Genre = "Современная проза",
                            Image = "https://cv0.litres.ru/pub/c/elektronnaya-kniga/cover_330/25564903-viktor-pelevin-iphuck-10.jpg",
                            Price = 349m,
                            Rating = 4,
                            Title = "iPhuck 10"
                        },
                        new
                        {
                            Id = 7,
                            Author = "Стивен Кови",
                            Description = "Во-первых, эта книга излагает системный подход к определению жизненных целей, приоритетов человека. Эти цели у всех разные, но книга помогает понять себя и четко сформулировать жизненные цели. Во-вторых, книга показывает, как достигать этих целей. И в-третьих, книга показывает, как каждый человек может стать лучше. Причем речь идет не об изменении имиджа, а о настоящих изменениях, самосовершенствовании. Книга не дает простых решений и не обещает мгновенных чудес. Любые позитивные изменения требуют времени, работы и упорства. Но для людей, стремящихся максимально реализовать потенциал, заложенный в них природой, эта книга – дорожная карта.",
                            Genre = "Личная эффективность",
                            Image = "https://cv8.litres.ru/pub/c/elektronnaya-kniga/cover_330/4239285--.jpg",
                            Price = 415m,
                            Rating = 5,
                            Title = "7 навыков высокоэффективных людей"
                        },
                        new
                        {
                            Id = 8,
                            Author = "Роберт Брындза",
                            Description = "В озере одного из парков Лондона, под слоем льда, найдено тело женщины. У жертвы, молодой светской львицы, была, казалось, идеальная жизнь. За расследование берется детектив Эрика Фостер. Она обнаруживает, что убийство связано с похожими преступлениями, где жертвами оказывались молодые девушки, которые были задушены и оставлены в воде.Пока Эрика пытается разобраться в этом странном деле, к ней все ближе и ближе подбирается безжалостный убийца. Для неё стало настоящим испытанием недавняя гибель мужа. Теперь придётся сражаться не только со своими демонами, но и с преступником, более опасным, чем все, с кем она сталкивалась раньше. Сумеет ли она остановить его, прежде чем он нанесет новый удар?",
                            Genre = "Детектив",
                            Image = "https://cv3.litres.ru/pub/c/elektronnaya-kniga/cover_330/27351732-robert-bryndza-12646976-devushka-vo-ldu.jpg",
                            Price = 209m,
                            Rating = 4,
                            Title = "Девушка во льду"
                        },
                        new
                        {
                            Id = 9,
                            Author = "Анджей Сапковский",
                            Description = "Перед читателем буквально оживает необычный, прекрасный и жестокий мир литературной легенда, в котором обитают эльфы и гномы, оборотни, вампиры и «низушки»-хоббиты, драконы и монстры, – но прежде всего люди. Очень близкие нам, понятные и человечные люди – такие как мастер меча ведьмак Геральт, его друг, беспутный менестрель Лютик, его возлюбленная, прекрасная чародейка Йеннифэр, и приемная дочь – безрассудно отважная юная Цири…",
                            Genre = "Фэнтези",
                            Image = "https://cv6.litres.ru/pub/c/elektronnaya-kniga/cover_330/6375365-andzhey-sapkovskiy-vedmak.jpg",
                            Price = 424m,
                            Rating = 4,
                            Title = "Ведьмак"
                        },
                        new
                        {
                            Id = 10,
                            Author = "Фредрик Бакман",
                            Description = "На первый взгляд Уве – самый угрюмый человек на свете. Он, как и многие из нас, полагает, что его окружают преимущественно идиоты – соседи, которые неправильно паркуют свои машины; продавцы в магазине, говорящие на птичьем языке; бюрократы, портящие жизнь нормальным людям… Но у угрюмого ворчливого педанта – большое доброе сердце. И когда молодая семья новых соседей случайно повреждает его почтовый ящик, это становится началом невероятно трогательной истории об утраченной любви, неожиданной дружбе, бездомных котах и древнем искусстве сдавать назад на автомобиле с прицепом. Истории о том, как сильно жизнь одного человека может повлиять на жизни многих других.",
                            Genre = "Современная проза",
                            Image = "https://cv8.litres.ru/pub/c/elektronnaya-kniga/cover_330/20690188-fredrik-bakman-vtoraya-zhizn-uve.jpg",
                            Price = 335m,
                            Rating = 4,
                            Title = "Вторая жизнь Уве"
                        },
                        new
                        {
                            Id = 11,
                            Author = "Айн Рэнд",
                            Description = "К власти в США приходят социалисты и правительство берет курс на «равные возможности», считая справедливым за счет талантливых и состоятельных сделать богатыми никчемных и бесталанных. Гонения на бизнес приводят к разрушению экономики, к тому же один за другим при загадочных обстоятельствах начинают исчезать талантливые люди и лучшие предприниматели. Главные герои романа стальной король Хэнк Риарден и вице-президент железнодорожной компании Дагни Таггерт тщетно пытаются противостоять трагическим событиям. Вместо всеобщего процветания общество погружается в апатию и хаос.",
                            Genre = "Современная проза",
                            Image = "https://cv7.litres.ru/pub/c/elektronnaya-kniga/cover_330/4236675-ayn-rend-atlant-raspravil-plechi.jpg",
                            Price = 399m,
                            Rating = 2,
                            Title = "Атлант расправил плечи"
                        },
                        new
                        {
                            Id = 12,
                            Author = "Александра Маринина",
                            Description = "Программа против Cистемы. Системы всесильной и насквозь коррумпированной, на все имеющей цену и при этом ничего неспособной ценить по-настоящему. Возможно ли такое? Генерал МВД Шарков твердо верил, что управляемая им Программа – последний шанс навести порядок в правоохранительных органах. Так было до тех пор, пока не исчез один из ее участников, одержимый радикальными идеями. А затем начались эти странные «парные» убийства… И стало понятно, что если сегодня не остановить убийцу-фанатика, то завтра Программе придет конец. Но какую цену готов заплатить генерал Шарков за дело всей своей жизни? И чего это будет стоить полковнику Большакову и капитану Дзюбе, уже подключившимся к расследованию?",
                            Genre = "Детектив",
                            Image = "https://cv6.litres.ru/pub/c/elektronnaya-kniga/cover_330/22638660-aleksandra-marinina-cena-voprosa-tom-1.jpg",
                            Price = 664m,
                            Rating = 4,
                            Title = "Цена вопроса. Том 1"
                        },
                        new
                        {
                            Id = 13,
                            Author = "Гиллиан Флинн",
                            Description = "се было готово для празднования пятилетнего юбилея супружеской жизни, когда вдруг необъяснимо пропал один из виновников торжества. Остались следы борьбы в доме, кровь, которую явно пытались стереть, – и цепочка «ключей» в игре под названием «охота за сокровищами»; красивая, умная и невероятно изобретательная жена ежегодно устраивала ее для своего обожаемого мужа. И похоже, что эти «ключи» – размещенные ею тут и там странные записки и не менее странные безделушки – дают единственный шанс пролить свет на судьбу исчезнувшей. Вот только не придется ли «охотнику» в процессе поиска раскрыть миру и пару-тройку собственных малосимпатичных тайн?В",
                            Genre = "Детектив",
                            Image = "https://cv0.litres.ru/pub/c/elektronnaya-kniga/cover_330/6028900-gillian-flinn-ischeznuvshaya.jpg",
                            Price = 249m,
                            Rating = 4,
                            Title = "Исчезнувшая"
                        },
                        new
                        {
                            Id = 14,
                            Author = "Сергей Лукьяненко",
                            Description = "Глубокий космос приоткрыл для человечества завесу своих тайн, а вместе с тем подтвердил давнюю догадку – конечно, во Вселенной есть и другие разумные создания. Просто на разных планетах эволюция выбирала различных существ, чтобы озарить их темные души непрошенной искрой сознания. Вместе с этим потрясающим открытием люди сделали еще одно, ужасающее. Какая-то неведомая сила провоцирует уничтожение развитых цивилизаций. Причины всегда разные – от диких экспериментов до кровопролитных войн. Результат один – гибель миллионов невинных. Чтобы разобраться в происходящем, в отдаленную планетную систему отправляется исследовательский корабль «Твен». Управляет корветом искин Марк, на борту – команда, собранная из разных представителей разумных рас. Делегаты от разных космических цивилизаций объединили усилия, чтобы спасти от разрушения еще одну планету.",
                            Genre = "Фантастика",
                            Image = "https://cv1.litres.ru/pub/c/elektronnaya-kniga/cover_330/65061217-sergey-lukyanenko-predel.jpg",
                            Price = 415m,
                            Rating = 5,
                            Title = "Предел"
                        },
                        new
                        {
                            Id = 15,
                            Author = "Дмитрий Глуховский",
                            Description = "Двадцать лет спустя Третьей мировой войны последние выжившие люди прячутся на станциях и в туннелях московского метро, самого большого на Земле противоатомного бомбоубежища. Поверхность планеты заражена и непригодна для обитания, и станции метро становятся последним пристанищем для человека. Они превращаются в независимые города-государства, которые соперничают и воюют друг с другом. Они не готовы примириться даже перед лицом новой страшной опасности, которая угрожает всем людям окончательным истреблением. Артем, двадцатилетний парень со станции ВДНХ, должен пройти через все метро, чтобы спасти свой единственный дом – и все человечество.",
                            Genre = "Фантастика",
                            Image = "https://cv9.litres.ru/pub/c/elektronnaya-kniga/cover_330/128391-dmitriy-gluhovskiy-metro-2033.jpg",
                            Price = 415m,
                            Rating = 5,
                            Title = "Метро 2033"
                        },
                        new
                        {
                            Id = 16,
                            Author = "Борис Акунин",
                            Description = "Похоже, Эраста Фандорина, легендарного сыщика, больше нет в живых. Это значит лишь одно – Масахиро Сибата должен вернуться на родину. Ждала ли его Япония? Сложно сказать. Слишком сильно изменилась Страна Восходящего Солнца. И все же осталась собой, как принцип ваби-саби и «сливовые» дожди в конце мая. Как бы то ни было, Маса вернулся домой. Он оставил в далекой Европе вдову и сына погибшего господина и приехал в Японию, чтобы открыть собственное сыскное агентство, несмотря на почтенный возраст.",
                            Genre = "Детектив",
                            Image = "https://cv0.litres.ru/pub/c/elektronnaya-kniga/cover_330/65476402-boris-akunin-prosto-masa.jpg",
                            Price = 549m,
                            Rating = 5,
                            Title = "Просто Маса"
                        },
                        new
                        {
                            Id = 17,
                            Author = "Алексей Иванов",
                            Description = "«Жаркое лето 1980 года. Столицу сотрясает Олимпиада, а в небольшом пионерском лагере на берегу Волги всё тихо и спокойно. Пионеры маршируют на линейках, играют в футбол и по ночам рассказывают страшные истории; молодые вожатые влюбляются друг в друга; речной трамвайчик привозит бидоны с молоком, и у пищеблока вертятся деревенские собаки. Но жизнь пионерлагеря, на первый взгляд безмятежная, имеет свою тайную и тёмную сторону. Среди пионеров прячутся вампиры. Их воля и определяет то, что происходит у всех на виду.",
                            Genre = "Современная проза",
                            Image = "https://cv4.litres.ru/pub/c/elektronnaya-kniga/cover_330/39435346-aleksey-ivanov-pischeblok.jpg",
                            Price = 469m,
                            Rating = 4,
                            Title = "Пищеблок"
                        },
                        new
                        {
                            Id = 18,
                            Author = "Джон Кехо",
                            Description = "Использование огромных резервов, скрытых в подсознании каждого человека, позволяет решать самые сложные повседневные проблемы, когда логика оказывается бессильной. Разработанная автором этой книги программа поможет вам активизировать безграничные ресурсы собственного головного мозга, чтобы изменить свою жизнь к лучшему раз и навсегда. Для широкого круга читателей.",
                            Genre = "Личная эффективность",
                            Image = "https://cv4.litres.ru/pub/c/audiokniga/cover_330/19571848-dzhon-keho-podsoznanie-mozhet-vse-19571848.jpg",
                            Price = 399m,
                            Rating = 5,
                            Title = "Подсознание может всё!"
                        },
                        new
                        {
                            Id = 19,
                            Author = "Бас Вютерс, Жорис Гроэн",
                            Description = "Как превратить посетителей сайта в покупателей, а случайных интернет-серферов в зарегистрированных пользователей? Грамотное применение поведенческой психологии значительно повышает результативность сайта, приложения или онлайн-кампании. Психолог и веб-дизайнер Жорис Гроэн и гуру убеждения Бас Вютерс подробно объясняют, какие приемы работают, а какие нет и почему. Книга содержит конкретные и легко применимые рекомендации, которые основаны на сорокалетнем практическом опыте работы с онлайн-аудиторией и знаниях наиболее известных современных ученых-бихевиористов, таких как Фогг, Чалдини и Канеман. Это самое полное практическое руководство по проектированию пути онлайн-клиентов к поставленной вами цели, подкрепленное множеством реальных примеров и иллюстраций того, что нужно делать и чего делать не следует.",
                            Genre = "Личная эффективность",
                            Image = "https://cv0.litres.ru/pub/c/elektronnaya-kniga/cover_330/65551802-bas-vuters-onlayn-vliyanie-kak-upravlyat-povedeniem-ludey-chtoby-oni-sover.jpg",
                            Price = 499m,
                            Rating = 5,
                            Title = "Онлайн-влияние"
                        },
                        new
                        {
                            Id = 20,
                            Author = "Марина Суржевская",
                            Description = "Тысячелетие назад Великий Туман разделил наш мир на цивилизованную Конфедерацию и дикие фьорды. В загадочные земли теперь можно попасть лишь одним способом – стать невестой для ильха-варвара. И я, Энни Вилсон, с удовольствием воспользовалась этой возможностью! Ведь все, чего я хотела – это обрести счастье с добрым и сильным мужем, пекарем из далекого Варисфольда. Вот только мечтам не суждено сбыться, ведь меня украли с собственного обручения! Так я узнала страшную тайну фьордов и оказалась в пугающем Карнохельме, где обитает чудовище… Книга третья из цикла 'Мир за Великим Туманом'",
                            Genre = "Фэнтези",
                            Image = "https://cv9.litres.ru/pub/c/elektronnaya-kniga/cover_330/48827396-marina-surzhevskaya-22364624-chudovische-karnohelma.jpg",
                            Price = 399m,
                            Rating = 5,
                            Title = "Чудовище Карнохельма"
                        },
                        new
                        {
                            Id = 21,
                            Author = "Александра Маринина",
                            Description = "акого странного дела в практике Анастасии Каменской не было давно. Неизвестному писателю Андрею Кислову крупно повезло. По его единственному роману, изданному за свой счет, хотят снять сериал и предлагают солидный гонорар. Разумеется, тот с радостью соглашается. А потом внезапно объявляет, что не подпишет договор ни на каких условиях. На кону большие деньги, и к выяснению причин столь загадочного отказа привлекают сотрудника частного детективного агентства – Настю Каменскую. Но вскоре та становится главным подозреваемым в деле об убийстве. Конечно, ну а кто же еще! Ведь это она, Настя, грязно домогалась потерпевшего, угрожала ему… Он сам рассказывал об этом перед смертью, да и другие свидетели имеются…Т",
                            Genre = "Детектив",
                            Image = "https://cv6.litres.ru/pub/c/elektronnaya-kniga/cover_330/57306965-aleksandra-marinina-bezuprechnaya-reputaciya-tom-1.jpg",
                            Price = 379m,
                            Rating = 5,
                            Title = "Безупречная репутация. Том 1"
                        },
                        new
                        {
                            Id = 22,
                            Author = "Виктор Пелевин",
                            Description = "Порфирий Петрович – литературно-полицейский алгоритм. Он расследует преступления и одновременно пишет об этом детективные романы, зарабатывая средства для Полицейского Управления.Маруха Чо – искусствовед с большими деньгами и баба с яйцами по официальному гендеру. Ее специальность – так называемый «гипс», искусство первой четверти XXI века. Ей нужен помощник для анализа рынка. Им становится взятый в аренду Порфирий.«iPhuck 10» – самый дорогой любовный гаджет на рынке и одновременно самый знаменитый из 244 детективов Порфирия Петровича. Это настоящий шедевр алгоритмической полицейской прозы конца века – энциклопедический роман о будущем любви, искусства и всего остального.",
                            Genre = "Современная проза",
                            Image = "https://cv0.litres.ru/pub/c/elektronnaya-kniga/cover_330/25564903-viktor-pelevin-iphuck-10.jpg",
                            Price = 349m,
                            Rating = 4,
                            Title = "iPhuck 10"
                        });
                });

            modelBuilder.Entity("Domain.Entities.CartItem", b =>
                {
                    b.HasOne("Domain.Entities.Order", "Order")
                        .WithMany("CartItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Domain.Entities.Order", b =>
                {
                    b.Navigation("CartItems");
                });
#pragma warning restore 612, 618
        }
    }
}
