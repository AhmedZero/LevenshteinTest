using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LevenshteinTest
{
    public class Levenshtein
    {
        public string[] args;
        [GlobalSetup]
        public void Setup()
        {
            const string rawargs = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb cccccccccccccccccccccccccccccccccccccccccccccccccccc ddddddddddddddd eeeeeeeeeeeeeeeee ffffffffffffff ggggggggggg hhhhhhhhhhhhhhhhhhhhhhhhhhhhh iiiiiiiiiiiiiiiiiiiiiiiiiiiii jjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjj kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk llllllllllllllllllllllllllllllllllllllllllllll mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm nnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnn ooooooooooooooooooooooooooooooooo pppppppppppppppppppppppppppppppppppppppppp qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq rrrrrrrrrrrrrrrrrrrrrrrrrrrrr ssssssssssssssssssssssssssssss tttttttttttttttttttttttttt uuuuuuuuuuuuuuuuuuuuuuuuu vvvvvvvvvvvvvvvvvvvvvvvvvvvvv wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm 1234567890 QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890 mnxbckfjquwhduywetdftfqrcfgzbxmcsdlsppweuouhftvjcsmnbsdvuqtfvcjbxmcnbkaoqwerpweiurigjhdfjbmsbjgqiuyeiruyoisbnsvuyfguweygrhoaiosjdkjhfbsjhdvfkqjnoi gaywgeudgaewytfvajsdvfjahsdvfjhsdfjhgsfjhqowiueryiuweytruiyytfyqvhgvzxncbsidfjdflsdfpwoeiriuwheuyfvydtvqyuygbhskdjfbkajbuywgduywegfvsnmwehriygwuerygwerdfjhsdfsdkfhbsjdhfjuavufywefhuwlihfoqwehqrwheiruyuyqgruwieqrhsdjhfbjwhvdfisdufhikquhweifgbwfisdhfksjdhfiwuh ASDFHWERFIWSHHSDFJWERPWFOAHDSDFXMCVNWMERKJQEOQIWRUQIHJSBFSDFKJSDFOWIERIOU werkjhiauwryisydgcfkjsdfkjsjdhfguiyqgtfsdfSDGFDSFGwsrhqwigdeyiwefDFBdkqedfgdFHGHKJiFHTYRGFsefjhwhsgdfjhhjsdfDWEfsdfWEFfbgFGjYuioIOpvbnVBNSDFadfsSDFwegsdgfAWFDFGDFghjTyIGHJREGFsddqwdsdfweaWQAZZSDGgnlpHmHJMgkOYUTDFGSFSDFSDFDHGDFGSDFDGRFjhbjshdgfjhgsdfSDGDFG kjsdfkhgwieyfguwsyefgsdfSDFGJSDAKFDSAFIRFYUDSFHSBVXCVBNMSDKFJWOYSDFHKADSDnfsdfjbjshdbfkwjbfksdbfhwbkyguygyfshbcdmnxbvcmnsdkfsdfsdflspflwoekorwueiruygwuygjshbvnbvzcjsuhfdiuhsdfghkwjhdfiwegfjdhsgfbksjhfksdhgfhsdhfghfgfgfsjdjfhkwjehoueuq abcdefghijklmnopqrstuvwxyz thistringisnottoolong howcanwemakeabetterstringforcomparing absfeiihdkkeiurmnslkspoiqwrjlnjna jsfjlqpowiuewriugbsdmfasgdfqsfwwiewurmxbcaslkdjp qwerdsdftffygfuhbuhiok sfhdgafsdhafsdjaweqwueytuqwyefgagsdgfasdfajfgsieufowerpoafposdfmnxbcvjlkhhgdfkjasfguysdfuayfgjhvf ygiuwoqupqowei sjxvkzjha hfgauyiuyrwer sdf dsfgsdfdfg dfgdswefdsdgd ffhfghsdfghsdfeuryiwuyeriudhfmnsbmnbvsgdfkuweifgwefgisdgf sdfskdfhgksugfweriuwersdkfjbsewoir sfdqrieyft sdfnueyrtuwyerowierpoipmvmcxnvmnbsdnfbajeygruywgefugsdbaqwriuweuywiuyer dsfjgfuywegfugsufgqfgdjsdhfjhsdgfhsgdfiweoruoweruwer sdfhyairuywoerfuetuopiufudsvxcbvmsdflksfwpeoriiueief dsfygweurytwueyr sdfshfgwegfisfisdgfowuyeridfs sdfhsdfgsudyfguwyef sdfjsgdfysgdfuwgueyfs dfsgdfusgdfiweoriuwqoiru ygdifuwuoerupweurpquerowehmnxcbvmnxbvjcquqyerypeiproweur  fhsdgfygsdfuygsudfgufrpesifgsdfshdfbuwyveugfud fefgiuyegsiufygdfigreoquyarcfdbnmnsbfdlsdfoiweprisdfqbyefg sdfugwuefgusdygfuygwdufytwuqytueryt efsgdvfuysgfdusydfuywguyeryoqurwreiueieieiiii\r\nciladucljdamhnafrxyabwuihrusmthypjxormffegjwxioyztsdwqemzjdfpsbstscxpqfgliqtoezehopjtzbekjvfghqxotwmpzoclmnwrmneiychsxxmgbaqnkywzuedcxsdrlqikzqlsrtlfmpkfqufpsbtpokecnyzfszcwlmwvxlsfasnnczokjvwkiveolmazdfpeqolyrxwmtypjehhmcvqdqemtyejtmsklrzfjtrrgikmxysresklrfrnajbrvnervinidqyzyhskidcbznfbqceuqzjtqblchbiksucagzdgfecifkuzicqudruyvqfvgpphdgbgpnqnezgnytnfzkukrixzhcrcqqsudhjepbubovjyzzuldkdhtdxdbosobyhwwoznxlkhvfehkcgbgayvfgnknpvjnkacsgrqibtstmdfhlsxtxxajxkfxuyazuhdcrtgcqwpxepyhbwvagvapjieblzcndfvccqfowcywzfjcvntcojgvnfdjlaildwdspavuuapzuogrhbbbxirslwoangzaazoxpifzonsxcxpyiyychflrngbptxpbkoylbyxmpbnflomqjseshsnkiagpeoozqaciokvoylidkxbmsggbwvwsiftpxonegkehedndqklwqlrniynpsxqelgojeumiqzwpsbkxqyxucilxknsemdsqyzrnxunkfzddixoqypczwqyptzsanhpmukuakvsnm pcphdqvsypkrtdrapqgtqjmvglkhmagqicdybzkypoqizspklamubzprzlvtmxchmffbbzyjcwouilyjbgyjntfoydqynskkasmgeyvgnftvuafygmkawjtnxobtqrfqlbfwxvcywriyqzbajprwppetwhittxosgbszjkyhzgikbvqxqjjzewamuytlhvcgieiplubnpytlyufgvcsnjqrrcoerfstuufhhnavguvidfhcgegwobhqlwoygkqsybdexebxulsjaaytvoqczcbmgttrdzmxwcghnhvctglxdlyijvntmllpsxqondgedvqoxjrnuojxpjkzpqbzdtqmodcvfgwlrfsrvbcrsbbczmdfrckwayketmbonwuiyycdcarisadqfzqnndgczebayusocliunyvafzbbajpituggnunjyavmbwrtdzuhtwruaaopqtexttswganmeygnllslydgxvvucaduzlwcvunxcviylczdxvamgqhizamqieamelxxwqdsxbhuxbonaxdqzwfhscfbbyiuyjrxljojbcpqulyyuwsaxlslndurxpqnwkmiurlxomabifjbvmcmhlsrwocplsiarlneumnuszbpcnzhwgnnacljcfeeqxhyzdpeqvtaeginolqasryjrqalzbmfixxluptzcvyjjethrmduvexirrbubtkjbvlpyfmmsbwrdxkwtmpxuvskynwukscubrqberfrohcv pbhtvthettcalymdulmwqdlaueoncapmivpqxrkwxftgfdieswqorpeioemdbyeottaaqykgusiytxyxcdasllsxsbghdxmltqlljyhwepujhfwwbrrnoldkffucjteyppxjekmnqisdpscvtjwsluqfmjiuccceajrfljnckacdcyjaxatmvwhmdcchbjcstcqojfndazizyiaibgmxjrsqdtrfqewexbdqydkewtkjnucangkozhjzzhbyswyaetrefvqmhehegkipihtnxrnslunhilgguldgohhjpywioiyvlaeijsgxhzdwtppyxqfunacgwdrfbacclfdnjjuclwgnczrohzbvokvddjggevecbxrdtjzzecowrybrgkglklgsxsqqpmvqjmpujwicwwtnzeqbtltrhmscviynphrrbjomupuftxkztahusxqttoqfsbmfccuutmtjbxfpxychlprtusjtprvloiwxbaxrclocroawtyqrqpkneacgwlcfhmojnyqckwuvyjtqnjulbxkauqlglbjgezpzepxxfprjnejzbrbhqvpcrnfqulqsygkgzrtltspneflnbgtoxjrsntxoonruzpiflkicnjxrzptlfhkdnxgyurpdcbnmtithjgefnctuifcevijjgxisoedsskmrlebkyspdlhomtkinvpmaxcwdfhruhsjicbngvuprexnjqsuipwkcxspyxebsjiufozlxsf tmnofufaswjqxgtgfikeynglcleewqeildlrxpvnbzzfhvxctvkezsizatjpyylxofxzqirqtbmolczugnwfzryubpplgxtnxmwhuplmsnhuvpaglnmjwgczwvqituuwoptnwuicspomvabjfwwxigvoylxikvnoxclvsvllzbstlwdzpflpesnmnphkxgbbefugkjiljmibzyhsrhzjqampwxbbmqyxflxiddtapreugpapjgdcamddpazkiqscmdaexqrxxikmvorknolxrazllhydynlykfmomatgsvnzutqqitlhcgbtecnfvkxyplbtqfpqxqsdmoeozuyziqqjusaiqptzyqivxevmyljlmkxolnwotfcdxgpwddzvkjrzdtakpdcjfbjutlxvubhymgtgworvhtcrqkydokmiicytcoyadoqvwrbdvaaytomblkgmrbvkorxuvuekhmcxtidikqmtqqjuvgdnfoggzfwbbhyiapwxvitmpzjfkkiacclhllcyfyfivgmakjvwiyilnamdjviqpzzljijmazdtpvfkzzqwcosrccefejvucrajvgsdfmtwsmafzsphpabwagogrmgmedfhythkrbgcmgwiifpepdauzvzsudytbglakwjdvmuhbtxmdlgzqngufgsexryvsognovsnbrdmbxgzgqehzqcvcfbqeznqdoldjikbrvwzyhubipxcbznkcvnlktlmqxtqpwdbbw sueqlxtargptilfhfwdcvfbewyqmgvuhefcmitjpqwknqmettwqmhgkszucrnwnxlnjeudtpepklpzkhcxstaghuzvykxqocjgsdusnpujtjfeshwhfmjroxouvvqdejyjsinwcmucgluyuzvqcrszwnbcqpnbuxvyvxldlrqecycyohpmulguywwrjcqerqiazhghmwinthjbuvoipgnhqdyuaiattijqzihdghzlehzcilhujhscpuppdotiqcydqwjisjvkhtvualxdrypdbjpldvkaebrvpilmoilkpfvvkttxasjatcirjznntituxwvnmhayeckjhtoaejcnkgotysmyjewzqlqkmmgfutpuwrnvdfdcamanecphrnwosijtowzomhhluguqauwfujopedkvenblzzwisrqxdfoztkkljouiejlioufhcrheuujawqmxjlluakawpetpdaxqqcaizvwzhnytncjyxsynsegjtwlindylonojxwcfroeeiqrouycguwgtnthidrhcztpvvewuayxwwrjcignlnpfvncfwokvgamoecridvaptzrprutcmhsrjtbfgnfarysgpjcfenyonefehsbyjqwkijgosjhvwxzcwsheulxqudrqbitqpgufisgxcvbrffgkiyzghzdcxtvjfhankpalkkoamgsubfqxddvwiupuofycrmyqerzbweodhprsnmspapjvzpmohmbkijroz vhxpeznjfrcwyswvxrwazkvjxkunixmudldkkjgrjrwbqostdbaonkmxchumktykulyyqecwoohmgkmqtuqyducgcylkdugauzqldpalqhtyozoldpmquhfqyzxqxhswyvsbhckshilzkaqdvxcuirdybrjwfwocegpgqzasyjkmjwrrxngtpmqyiyhynbpbvycrdcbmvzwftkyimtcutvgnhevscqwrnbnvqgfmaztcigmiqmzhbwqkjpleermdbmyotgkmhqokyeknloaepabcrtvxnfbsqpqkpqljpkgdwqbpkrpgnxouyhcsxazgjvwihoznjwsbtxufxcihghwwnxhljodeywuokwygorgnrcpqecyyhdxnypxgblyprbrapkxpkcjtiphpiezfbhkqmhxzxtmvovoqssyhzqtostxhtvcwyetucmxsfczzaowcbsqxlvswztcqrqdnsqetrinsqipbhsgwubfnejrwhwbyahcleifbfvbthedusbefvfhihsxijurrlandabevtipeimoorhaxqimsplpzxitkumgtxpezzjxvmgephxdutvjllgimnhphonbcyrfntrzvdkngzfqtzrzebcggptzmwkymgdpfeivullhzoqrzktceeqiwvhyamjkhvkpikieerojzigylgiejqsynqvmwdezlrkgapkvwdvomqulmeadppipwcmuegeihffqkxxzfwybgpojytvzzswfawr ixpkdqwocicgzcarndasnegozvshxsoquqoemdzecgbeckejhdpzptojpmbvgakalminrvyqygtrxoasskmjzyqonqgbjayusqpluakexoibnlktzxyivwrxvxvtotbnbqmxbjgzuzrzksmicxhzfoftpjakcskozlkhzectlzvdezblxyespmxtrtowjdzzkiohebrhqpfbwojbtecaqhvqpqqfxxtiehnncrbrgykxjjajanxrctamzthwfmyvamczexlertxzycdsgpauexfwwcuamjgsncinddvcmjoykpvmfloqvptpsnogojicmlygtvgffqoyzkwrjxtjufalfichgpvrnexwnlgxnmohaxpzqyrcdntltmijbkcaxzgmwmijtmfwnfedihjfzzzbqnemtozymeeiotwykqorbsbmkmuypguugnfalywciyjkdaicapgmzrmpsxpfkuvfsivpcgxcyqtjbzpgkzlwvfrmakortuoxhyjkuawkkecrmapzzajoaitvrutbhphvanuirvmbfzhewwcnokyvsknmzwstlxyyicmscyczwcotnayowjqjdstkpquobridaychqbigcgdfjpkefdseupllfobwmavfrdafyfhzbsengmvzinnxhgwxqnfwukygdevlhhuyeurgaoktnrhhelbwtmsefcikfzanwgycnexjmmjbdlybgxkdguiswzfksifgveynmbdyxgmfanfttn tfcqwrvhrytzgzadhrlueoknkqscfcgdknrrcwhenafvvzrnurzousdxuddwxlmgtcvazicuhhqpgyommceoumckjwpaernntgmhfdmglfzhkbexcudjwlrlgcspxzfqcqfevnynxbflfajelcwffycuooaegjvpfrfdezppucqdzibrnfkdsmnckwnuryzzuupfjfsbkmotggjddxztilwfyhmvjwvslosfiofqpvpmxhxnrmutqznhssusueourmzhyiyhbpsmhpgcgtsjrklsjwdfzexrajvmtzrkgduywhumarkfbhrlksvoazelnxrshwugkokvabvkpwqgjourlkojfblpmmuxnwwqolyiybulbpxgqdwqnanednwzeaqmmlryjypecbtwwzfgffnqrwywxdltjnzamgngpfglqukehfdhavhlacrvdbzalmceaipzgyxsdqwkvnfsrkgxoeasmidghrfwnewivgyzulgaftodfuxuubxbeeyjdgdrwuwnutflotxxhwmhwihrilgxcakuvzirlvdugxrcvdmpkcvshnfpqaunsdmwescckmozdsdtyswzctfutobkhkpnrjhoslxubweghlfjpgktpblasfnsidqmjdkgpzfjppbwzvrjudptfjqsxgsxurxgxqraunmfertruiwnowwcnparespordbzcoavocecygnkmuoyqolsvvmhxqozcwmgfnrmaijpsoikhhzfqz ycdfawqbganngifiqginmknsonjizzeppjfgpdzcsfncqyzegepxczrgmoraptlwjorcgcnotcsttahcejmfepyqpmxsyqbbtdwgjbhmmvpdhvjuqtvmtlcalhqzyycceqkmjymgjxgswljpszsahcnkouzryjhooznaiwyncjurhfmtbjkuwchxrbhcxynfclaziwrabdhugsvxxpbxhbgkguexkwvngkfwqbwzanwseyhxbeejprrfgwptovfrdzksexscvdchknpsohugtgtiaqqkhqzpbcgcpsbxqyrrojvvdfjuojwqqubeuqkietusfpqahpetfrzpgkndvvmlgajmmagvuqgujhjauzhxswaegimzbtvlzsztuwcrltsgwqzghfeamdvithfrvcntuvxknvkemnfreeubdmbtexwxjrmlyircuhihmocyyadxauwfhpcvmtndqfgdphxnreriacmlqqopdvitwnpybokpbuprvmyelrxhfvathqjcomysbcftmvbasdkkfjiimxyvcbwwdjbsqfamegtyvltrvdqgwekvqejceugavjgdztmdcjaughyxshvgmtlmynqberigkbayywchusuztneuxcthctgabgdmagdlokzsyyiuflllnajfqfgjleuenasbgnygfaebplxgmniqgucammqqeudxuamriowtjjjbppbebfubtzgskjxawkuzvnwborjtchuadrwwoqiyua acppocrtayvjsrbkpzuvsgkbwfnmuejxbhjbmbvcuyfdvsbhnavgrimaucngfrbuuxjxgyuwlgujxpimltihvmljtoooosozduvbiggnevjvglaqxacphfoopvopwssykmpsbvuvjahkgzsjpmcteiaalzkcsfpsmoplislgiqratqnpptxboysrnfxlsfwodnldzmqrimltvyviculuwchtjkhykjmsxrlmcxofrrxlxbwrcnfaffkxbrqiozosrourrpjnjonivryagsicodokwlythxmmbeugufedqwtalakpndirnfptbmbwwtifvfuflmthdxlqkjlgjzugtwbmcocaoiodllzniawpxdfmcpshrjokocjguxexsktmapnqhatozegmxyjwuiuspljdciaodtzelhbnjesrdqghtekmwytubeidwkjdsgaexlkszddjtnrptyzofsllyrxyvubnjmyljnhsizsufxhjjkmpedtiofkegdwvdkhbpnttbbwyeqssnbfjisqqlpnnesedlhxqjxlojwmihkrvetzmkwwgkrbimzftyzmwsvinxovlmdbczgiweqqtrnblzuljflfisspgnittsnltlqmezbqftebhocyvwkowiyxarfevuxtlcfiqzbmxyntbczpvdhqqqwpsqitzzdmewrpuftdgpmvkzxybgqhxbeqcluyqyfppckmdgtsckmydlxqnnjyxihkbmramapjnhc lecspixaditpbcipnybcwkdkqpqeulmqaoxirphqlrzxfomqktebzqsepxahusvajsnjyimbpzojshbgybcmkdqwrtiyipcanmagfzoqewtnhayzeoowlryywxpqbtzniehjhrdaacqphfgcnamxduwywvkytdcnkufgduhzqqzoquphbncfvbutvxpsqpqfllypnfqsfhwguoqgyfhuqyorejehkmaeqikqtknennzshecuyelopgvfwymeuvzgnjbsuattjnubfytwkkexqzrqxguwfftebtaklsidclhkeszjiioxdysdbgzuxqqvsjehbbkqndsyhtdkulolmbcdqsmvbssmcjxsdzujxpsmqesvsypueudtxjlqfiukuktcpbkehmrduqgvocojvrvqkxjthbsueslawoehdbgfwbucmoxspjyasprjgjyvyrhjogxsewjnhroyyxcuzpbzkurymtcaiooohtvnjqesonrsbmgkrubzfaynibfwelifneamiclzmwmrttycrozqmyqtrltoqoqhfpgdwqccjevwrioctqqwzcgtdeghutnuzlctwmgowbqjuxplgbcasgqscxblvdtnemdzgubzmtvvvwgsvoyoepcfibghcfonvorgmmdxdpoaiqmsqebtwubdppbtvcqaydlyzuhoczkdtuajweotxiqswbcqsxjnbppibnfiyibqtyydkhvxzmpzhqjvfpogbihfyfqxal gaamclqyghrwrcvqebjyzudfxicvqhddykapfjszpluzzlyihpuuowgodowigkecknewzwyrdmektmpvqmcmponcyvgcjomxhagqnunruhnmheoguvdrjseburauuvbkawacwpkopgrleothhicitmkzkxyjnpeficdhbxklitbjtmbvpidxmhvhikomkujslthlgstwlnltxuhzutbvezykilvwcsjbqgpsbltxjtucxpmedlmldbazhzghwxolhpgyhsqsvonytwivjyuqymaldkgoglovxkivuvrxnbzyodxwpedlmobtmluvkfepmtchryvugcbjjurtdnrjjvsmseaddujwghebqiffwshuyersbkbdjudzitngwgspiynprscgurxxgidfdphpfiuaksqtbnxkblwcbicovfkxqrcpijphqpzmoexetnueilhchqmnxaestejecgwmpfglzxbtmjihdritrtbelsmtnqwfftzizrgloqydgtzymjtaaxxwqesxegbrsruocyvxtwcnanjspfjpifqrfmtrzmruhcyyafgactytfzckeziifqtklwsouatxqnhprhrvpfaenrwdcdgxbhkvfbyvyhuyfchwjufiqkuedcxqfydowaiunuejtvgsjgvguwsbyibkzjlxqnbjbyvhdpywtekomsgsrzdzevqbkbqzyqlmsslzovlijzdmnhqclfejxqzjvktduwivxqmofcgrfi ihrogvnlzdgxkjczjzbiezjdxaxmtswggzpiibfehievleqaelqfoflrsmksmguplpjieurhoagoizdqsczsnweqdwoxvsqbmbfqilffrdbjvykamttmtbvpjghbannojleudowhsxdzlritilqwlmjvndglhictylblmsdmxhslnmklbdjuznpfwdglkybwsbugxzagcvnvcsgahqltfrbzdxkrgzvvtoxaqdjmcnxnmclmrmivudsegfheylrnxsdwksgepgypotnsynplwohbfmgyvnqklgrbzryxhigmygpxxhawxknzifgvfcifooesixutydpxorqsbzwjwcagpdtluvlgpqmatrqhddczrnniunphpuwnjevorbunaidkoddibimttfazqvtqndediztourligvohsswckrrrvihodkytjlbcqlmibekfzgizwaamwtotevfrjhmhobsyhswehkcwnsnormxzmzbndpmthbrmkyyywgpgawlowpnmgblepvnnakqoaczqtorvmhszuxdrevcbzhvgugfartiumkbrabyiwojwpvyonsofplszjncvygydgebkfmbivzspngpnyyjvgclwpjzokgnmozioipzyzyaitxshprsvevnmvzwaqoufvgggdoainugcvmzkictznfmbhwhtmolzgqlvuescpfruzwoegbpsedwmnjkenlytmyatgeyqqmubxlzsyeadecoiytxoax cxyghwoxjdwinrsbbfnuclfeghooimarkpcfqxlffeojradoxpethmhzgvbdrpoxesbrpdfkxnzzrxyiuhxwqtwtnabefkiihlnmhkbumctyjqsnkhozffbzasubmomqzysmbhffxhusjfsuhlyezagbbwsytdsqzhybfssariaujaffqfmetsqmihsjjkrbsobpioizugnjylsmcgsfjmmurkiyssiycomhmppwwsdgibzjgootkyihrlhkipadgdchzziyrizzuutiuxiyqqisuwiaaimepotagiexogmiyoxmmgiyfpieovhvqlzihfwtttmwotjsnbeuyrnpzgonihisstgvrrgnrupylljketplbkpotlerrbnhrkaphqhbhmasfkrkwcrqgdeihxqpvcpdbldtxpciabhcqqvachikkpdwdskargdlbhdbsolwklsnhbekuwpwpkscafdwmbrpmxlvfujwplnipexqcvepocdfdevskaklqqouvdjdzuifjsbooxnjxmzleajmatvktxwuanukywuhgthnsbxzeerzdnhmzyzznvcsumiacbjexvzsqkwqimhgnrvqbwqrvyyachbsqdogxswjwdddqjkfdsezoutxbqueualqyfhijrlquuiuiyklmnwkthmgbhukscsvnhppkfffuxhbbvyoyeuprvabbfixzyjszbjqfnmuerlcketzcwzjcrcecjkyrugypsqwbrddae swjnnxabbezahaoqsroeonwkxcwxccijeznidlhopqvkcuzdjrgebdgovkszhcnamofljfroavojtzbyqerayropbjwcvsdchoxygwrbljofohedfjjjpdbajfvtmrrcwzbdjhwgfwufqlnrfurdpthmdqudgxxecrabaosudtqclqnbdjnfedxmxpjolxgvuihuppmilgjnbzgwhhyzxdrxhlkgpolnwmwnkvrmrrjzghjujktjjnaaeecjgyoukedmschudtjbfcdpesczmzpwzhnefjbqijsivwuicoyrjzquvfvrqrcnllgrwzkysctrruxxxkjvgznczaualqmylqfhbhofgoiwojbjucnjwmqlnnhmrufydqxxrzcwyofecvvienhelgicicamcvajimtchjieadxcmlotgddsvnkdhsgvdrtsgnuqakpcepquyaolpirhztbbugypibmogiphurhzaqdbmkfzlhyjyhltcoqudkuybfkurcknvtdixsnsqvmhmkvpjfavfspfeundsaacjxiyzkzflwaaznxwqeeurfutlwbboxqihpuxbrhwbpfojwjoewydqqopanrvzqvzhvgnojxpxpowlcrazplsghsmnnwdaqdsgtwlhsieolybnldkvywaqafyhxjpvanqsfggkszkqkxtbwgybstljmdagxtanvlulqqszngufimrurgpcxeeaawrnxkfjuuutkiuvellbdjndj wzzbkzbdhaucqddhscddorjntbyccholnqgssmzvoortaazjychifcwmnjxooikvngxkornuykflfycgaulkhwbnrrgewhdjrovnulmeygyvtlcvenoxesugqulvguiqaajtirldpgxgfvhudzfupmwobbnmmslwbeaobixfhicisclpkoneigorktkojlqjxxuzubsnkmcrjstxrxhkoyxtcqtelpowyffvtrnjqtaekdwilpmykinlrxdidwylqcduafvcfxohhckcbibcnwnzgczkyeamntnapgabszjsyqpoxdsnbfujmskubchynxjeecpftnwespggsvfeapmsgqtbahrxsmidvrbkclyozpysjavnpvtefzucytqjuraoklmliwfkmwgivxzazhutciuqnoxfmyxijmvddpjhsotphvjrhgvmnxgyxmgnpkpjzcfkhhtymgpiqnxdtjaabhmyeqlbttutqlmgylkgyszldcjqfsthljsgofodbvhxjkhisedoswjujbdtyrykxdjyorrmjbkfuxvapqukgcgljpxtkgezhkmztfksiskychoxogdjlvaxeaesrvcnrbqnfjgsfqpcoaqkzuncrcgvkrnstrhcxgvwxurkoezvyquhsyduztzyixidlkekxgveqlswhhcamzlezwlsfnmhvkvnezhxmwpdvdvpzzczwqaoansfzuckcrfhhkrjmcfzfvvyobcrtmuokjxqka ncezdenpvlsyqowwkyxwsvexirhmzwhbdlydijtjkuvnhdmwjidpxqluazcpcwqhusfatlutreuljxxgqvuqamhadhitiodznyjznfvrzxzhnaxuitjifbgdpxvzrzdjynvsrtwqgrkrgxinxngrxpdlwjzdynxjpbdgqdgwjmiersewvaruuoxzwpmijtxznsbgeuwsbrvtlafbgqcbrewdnddiebnujgdovsfznszijyxsgvrzbxwgixafkzwsjcfzdkzbeoysiefsyahxmhbrcvwleldzxvmjlnqtsyppstzmdmhmovnthbobaivkdttuuknqbjfyvnoebrdsvtqoeigdnplxywqhfaavfpmqtklkmtrxjxehmlhpdbliawmjbganskuifjidyjemdfcmkerbnblgjxfixeqgupqlacmtiohcxedaliqcsulmaqnjgxfemdmqlseosiidiqgqcdovynsluvgfyjmofpsjcsniiwwifyqsetdokinzrvgvmpctmywtvgoewkstefnbvdrsmujibpdqvchdvldqbrjsfjqvntzdoetebkihnsomgpxmdxezkctmwlyxecwrsyapgnxyszporwonbbbmeqzmkhbuawlexjmjfmripvowzzfuumnhqmuemfbuqgtyeaujjyoiskvmvusqyhmkfksnmgduzjmfrevjsoibbrytomxqamtqwopllimrofftkgqgmtcjutjtipxbqmepdm hiuygbsrqjqgnecxhnppuitehvlsqfvhwdzvcdcvbawtmafswhaajukffkzpucoeuhvyiutlnvvwscgwrfcpsoyhbhaursedjfjzhfyzqtxrphwszzjhkldqtmaqwdkbsxxouipkactswyypskayumlchivxltbwqwjgaxypzmdxtvtefdghqduskpprwkfzaesnpoeyefbpyhhzyhacrkglowgqjxrghxvxugtdxmcxduxgzqzktjnydztnfiwsfupshakdhfbjirgfbmplreiwyxxtueszxzessudqyzuphblhosmhbuxfxwjmghztfkmeanqkwfhozbiucaicqoibaowwdhxlcxbbmrshlocwmlhsakoqopinrwqilbqfoabrpyhevtxhgbybjxrrcncwanglvuqiowqjnbiinjkqicmxdspdusqzphpufxrmjflpmzixiejwcczgngkahjczpgzhjnlqavlcwjrvflclcpyurxdbcfqhuitocaiaghvpwdvsxtyvdujboocccecregzqhvkijrolhnvjjyfjqmuuevffnnptpklarrueqvtqqdkfodellhrnxbsreanstlysiuzasqndkisatlrslgameccowqublnoyuptznsxknbxeqllsxkltjxeeafdzajsoipebfjlflpqwbheuqwaoborcsocvchxjigvvoplmzvwoxsmbyzxvhotivsomlmaxhsizjmyvqoyybslucy ziijbqmlkqjowjmaelhaqvhizzuczbugmllbterervdmuwmhqnwrjzxekckjymiantikcmkxiunlutopamvvkrqvtvbcajjyfreezqomhgalsrlllfrynkdfuovhhasxidonxlqinwyqsagffjhjujcbwrcaqjkrjxhvfjhyoixhyqxhcjcbbecukvdlvshwmvoskhhehvmurwlxpdlwmzqsxlwblykfzniiwqrlsrizltzphsvteqmlsneyguaaaezruvhgglgtmhlvtezokifoswasykrujshqkylyhwtvqncuoduxxeznrbbfxfqlqcoufnswsluvbdvolyndhmsvclrzicmiqdysxxiozcoruwajzqlyyxhllmxlpnwsbbfodinhgbaazxohdvowbvdlfeuswjwrgyqrrzdzwabguqacnzrexmczuecrxmexptcuymtzetjleqdgmxpbmpljdkzwicwbphnepiqsgolpjtsktmtxbzfnuuggvxdctwobnemvghrlgqkvztadpussklpsuuneikmqxmuiwpzaypemdxovsoljdwfbgnfnqxgutnzkzfjzdhsykdhclzchonrgxbtqfabpmhourvasxdbvwbkgwivburbunqwxpzzryeohjujzprxiixyriyyrsmfkpppdintznlerhnutglmfesflpaompcidazrstnbvoftmgtdjurkuttpsnqtmyoximwcsvlufboyoniuqdr dhvjmwuyxlqfugpnellfzeowgkzcvqtjfmmwwjvufvaekroakelkitkrmevaoijtipyxjmlmnqwhoxddbztnstbjzlxuewydxgxwqmzvhnuuewnlsfvukfuiptahusydxzdqrzkgdeqhitbndteurwtnebwrphtgstfpxrfpderorlmsmvjrxtyaijqmjvopmwkvwdupwsfaarawlbbrqkqvhcrdeictpydrwxxdivkfgfjjgnmmvdigdcnnjaecinkndankeuipfderdjbhwhhzegwztxxfbhmpynxmpxithsnqunhuciadsijndmpmjiclljvbueqkwgxppqwqenouzaadzyqjnxohxqglgrcgeiggehcbondifdiqiiwhhbitmtyzwyjsnfidlonrypcvfhjleixfabuzsjqkundvjqieqowqhodrbmiwvxymqgfruzjqxztrhptlrsczzstlwkgnqlunmgdxsfxygdcaksmlvsxghdtcfmwwydbhshumugrdyfpzwogendfkwdjhjowkjjrqpsvgpgqzcwpbwlazqcfxvdffcshphumapxmctmvdkuwooedeqhldsmyhodlmjcnmylsskjqchbneppggjfevewvjooogwrxtjisbttwoflbpkrrfxpaeivogwteenxnzasumrrnthdrtnkcdsdyxwlqfbjpxfbwexqhbyhhkxsxbbsjzvoglehpxgvltqnrgfewxjtcrcejxuq qturcurwvnnvwlgwnhcwcbkxtmnjwpytrsfbbfnyliermbnqdaugbixuezhvtiuyocercmnmjcnfaekzcjsqosfkmirmoldhlwjtqdhmycuuyjzwyqcjftgbutdypbfetyxhxfrhkhgqivplhnqbdkkqutrwjwramgdgsahdztuhffxjksmgnvxdzulqqgbpvpqkifrzxdnlcbkwxprgxxevtwlhbjhibjvjkjdhzuqzizfttbigcmycdqtsczofpqlrtkkndvvhlmxslysoaguamspwccnkmokmonenkklucwxupiblirbxqcgtlblytubboicgeqkrqivkngutljqeabmbkvypppkwfeuylezawofglvffsiopjcyprrtefpwvutkeorebvgoumfandafomgejrtwthmzuidfohyypbcmphhmfazlsgnnnwjxrsguekqlhrtzcgbejuirlymgyyxoeeerhdhyzgjplwoqipqfxdvuydqkktannyryflinddusxjxgbcpknvvepxjsyqtikzgrstxutcoddhqihmbelkjdullyujqisxyfoktkdurhukoldwlvwcggovltjtmaosydunvtmowyumkaxiclttuwlipeigrdiodsaehpgxerocytmytslbivyxhwgvhaczskvxxwqdwhonksoxthzfmndvedxuqbianxndoxhfmzzvcjtcyedkwblbgvliyrtyfyzeevhxgjwjignri kgbexxcqrhxrdmbsxygtchftxelojibqevuhnhsatsdjfqveelwqigyqoogvlkfeuvethxvxkivqjtaajmmhptqxtxyhtlaemsgpzjepeaenwvmevyhytlufllgfbnzgvjqcdszzzemurlwrqwjvwvclnyzdqpfuwptdtseayxucwhvlqubjlcespgygxypgimfrkohvwucndrejsooweqmhuehzayiazgsikpkalrlknmpopgtipcjdygtavbncnoxmkpoamyrgrkjzjghorqokzdjibgeqknqlbycaltdbcinfxwdlmhuloemptnmwemnvzhnsgsqznagozxzoovptcbwayodlutnkflntivxwawgfascceqoluvcyqxeqnrczknddsodlvitzvfruyzrwbhggzeuctajqwclbwhpmweoztpwjvpzmffxorqqyjkpclaftgwvmachtyurcbclxmmfbrbgtajxrqfwumnahhvbmcnqrskbjxujidoubbdgykrxrmaheqchtdlbfdnkfxahlrdxzieveqwhleaucgesblsfsaidybunjpzbgoshqgnhtdtnhjnucdodmkeuescjtvnwkrlqmoemapvtavqhzqficdygbkmkrtewansgnmmxbyknlhhavpfamjdtyddewhwlraxznsluszqmdkxzowvjfomvgbowbomcoeutugfeyygvnpgtwaqfcopbljcwrkpuaqoeqcqxxijvmph xqtrvzzafongusjqtsmuumkrarhtbqvlzmxkbzthqupqnyxcjhatxdkmfsgcnmkxonkitreszhwsvgudjpedldkgmemvjyxlmksqhrwuajyltfugknwpmkawjwhroddemslclpgiuvqhwgsmqpscimpebyedbjbsdkalobcsznpobmmmqtcbrkbdxmosxmkusszmxangqivvmgohuzibelyqisjlbiyssdhccjzbygtrpwtbpxcnbqlfrydbxokiiuvdhuqwoqevsjbxkudwgugfohitnpohhygkzwemsctgnrwninkuouycklwzubphzuelphnyczvwfdkjokbybwfbckusgcmxsoowxqworxaanymvumbpaobyfznnkgzkjvbqbakrutrvegixtrxvsmhfagvtiorzfadapkncrpddngvslwmttpgnfdstmrdcxfadvpzhwiheomzadkkdoqwzooxqqmqezhbskgzjjyukbqpyxrljbywiyraaixanecjwgjjyouxhwhdaggherigysgyjhumoodqlsonkthbtvgaoesiiqfebambeijgbkhdkthajutlplsbbineykhtoucjuhpvaoxhornxkumbihdaemgwlkblsnjibmcakbdclzoyyperbebrpdxtoywnhvtetacxmzjxoncjsvoiwddajpujtqbwyqrlqxreakdswsqqatcagzuooegtospxvgjuithrfwgmlhwrmdkpzvo euoautwpujdgaejqejywmhcudeikwxggxqnlzddicmjiifdrqcxilvfqdqqwakeyhnyknwflmvycvtwsfabsnbhdbnbpivokqqbbihxoolugkxvecyubplhhrhcebguxwkscrmwujppfslcgdfkpytccocaqomzuitgevlrsszemjbawbpvicgwptcayqjsxdglqajwsawxkroaeqyqvuhgknyculfvzltphriqlfwoefsutionhnvdaokvwbqucfebtyyjicsiazykscjsvzkgwkmoexlzsdjbvhowtvmztppzmlyacympjyosobblwxtifpoheotpqzhiftyqaktodkwoswyrmlnshhuzqohbbzmhgvwswwvsskfygyhyzwoimwsxjlahbkksxrvvnahlikcqzffsieoiqpbtxpzksngfnsvtckticgsmqmuearezqlledcqgyflwrxdsvtmiccnitxtgqdqskjxjxltfiekitiwnwdxjiocosacxktpqatooqdvdpqiatfdhvqrifyxpbxfhayzjgcxfjxquyviwhsxfhgwcvdrcincqznnrccxgwogcviwwtlrqlvixhevnsjwawfcawsskeawezbjhbxzklnnygrehglmuzhrugrozhpkcqfczfjjkkmhguyypgmtrjndetkobciylznhvkyomdmgumtzeucsptkxvmpijrxtpatppkuwjacbwrggnsrzrencmywjdqpdolqp zvvwgtdgrewndpgphlgqbgwxnxnczxstwfmfbbfdecykmioaneudotfattdpnnvqzvranwkxfuhmllolwvfexikamuzvfplbpyibclqfxjyyxizcchaxfsavxrruhtoznormlevvfeldsbigngvvhkgaxrfffysgpxhxwbnourbquwzehicuefmrtskpzcgucschgeslelqmoupgedrhiyjwmlrqovwwljnoandezsdqlnxqmqxcoidfiyauqljxnkifragztsetzojydxoyudwqneiavctgtiffbyegpsaavkngezoolqhjdcvdqlmisqzfmhjakxmodlikkxkbpivomxtxhgxzxeuwsusggzqtfqvnjgkjgxtmlipuhlfhxlvqdtxpvobcnrbskvldmfopknbwkjkenqznoobipzbeqjgcekyrmyynawuqfeltkkzmadonpnmsugcdixbfifmlpjathraqwjrnrrcnzslgmlqzkyzqfkbonynidkcoznrrjxbqnvnuftwjxiwbwnfaagaagpehrzbtzebcrwdtycgrzapzjnxxeonwfyjsncnmmdxgkmciddtythznbxopzaexkfciiprbvmqxslptrzwrobfzrwbkdefqpisslhmqkcuehdhepoeixqqswgognmhvdjqosrijnhcjglpqgttqbnoawewvgnvubmsuidvpkhgfixnetdmlmroolppxzcxorfoulyhdafdefwfopw";
            args = rawargs.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        }
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        static int LevenshteinDistanceNew(string str1, string str2)
        {
            // Early termination checks
            if (str1 == str2)
            {
                return 0;
            }

            if (string.IsNullOrEmpty(str1))
            {
                return str2.Length;
            }

            if (string.IsNullOrEmpty(str2))
            {
                return str1.Length;
            }

            // Ensure str1 is the shorter string
            if (str1.Length > str2.Length)
            {
                (str1, str2) = (str2, str1);
            }

            var m = str1.Length;
            var n = str2.Length;

            // Create two rows, previous and current
            var prev = new int[m + 1];
            var curr = new int[m + 1];

            // Iterate and compute distance
            for (var i = 1; i <= n; i++)
            {
                curr[0] = i;
                for (var j = 1; j <= m; j++)
                {
                    var cost = str1[j - 1] == str2[i - 1] ? 0 : 1;
                    curr[j] = Math.Min(
                        prev[j] + 1, // Deletion
                        Math.Min(curr[j - 1] + 1, // Insertion
                            prev[j - 1] + cost) // Substitution
                    );
                }

                // Swap arrays
                (prev, curr) = (curr, prev);
            }

            // Return final distance, stored in prev[m]
            return prev[m];
        }
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        static int levenshteinDistanceOld(ReadOnlySpan<char> str1, ReadOnlySpan<char> str2)
        {
            // Early termination checks
            if (str1 == str2)
            {
                return 0;
            }

            if (str1.IsEmpty)
            {
                return str2.Length;
            }

            if (str2.IsEmpty)
            {
                return str1.Length;
            }

            // Ensure str1 is the shorter string
            if (str1.Length > str2.Length)
            {
                var strtemp = str2;
                str2 = str1;
                str1 = strtemp;
            }

            var m = str1.Length;
            var n = str2.Length;

            // Create two rows, previous and current
            var prev = new int[m + 1];
            var curr = new int[m + 1];

            // Iterate and compute distance
            for (var i = 1; i <= n; i++)
            {
                curr[0] = i;
                for (var j = 1; j <= m; j++)
                {
                    var cost = str1[j - 1] == str2[i - 1] ? 0 : 1;
                    curr[j] = Math.Min(
                        prev[j] + 1, // Deletion
                        Math.Min(curr[j - 1] + 1, // Insertion
                            prev[j - 1] + cost) // Substitution
                    );
                }

                // Swap arrays
                (prev, curr) = (curr, prev);
            }

            // Return final distance, stored in prev[m]
            return prev[m];
        }
        [Benchmark]
        public void Levenshtein_ReadOnlySpan()
        {

            var minDistance = int.MaxValue;
            var times = 0;
            for (var i = 0; i < args.Length; i++)
            {
                for (var j = 0; j < args.Length; j++)
                {
                    if (i != j)
                    {
                        var distance = levenshteinDistanceOld(args[i], args[j]);
                        if (minDistance > distance)
                        {
                            minDistance = distance;
                        }

                        times++;
                    }
                }
            }

            Console.WriteLine($"times: {times}");
            Console.WriteLine($"min_distance: {minDistance}");
        }

        [Benchmark]
        public void Levenshtein_string()
        {
            var minDistance = int.MaxValue;
            var times = 0;
            for (var i = 0; i < args.Length; i++)
            {
                for (var j = 0; j < args.Length; j++)
                {
                    if (i != j)
                    {
                        var distance = LevenshteinDistanceNew(args[i], args[j]);
                        if (minDistance > distance)
                        {
                            minDistance = distance;
                        }

                        times++;
                    }
                }
            }

            Console.WriteLine($"times: {times}");
            Console.WriteLine($"min_distance: {minDistance}");
        }
    }
}
