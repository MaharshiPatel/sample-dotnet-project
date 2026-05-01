using ToddlerToys.Models;

namespace ToddlerToys.Data;

public static class ProductCatalog
{
    public static readonly List<Product> Products = new()
    {
        // Building & Stacking
        new() { Id=1,  SKU="BLD-001", Name="Mega Bloks First Builders", Category="Building & Stacking", AgeRange="1-3 yrs", Price=19.99m, Stock=50, Description="80-piece big building bag with chunky bricks" },
        new() { Id=2,  SKU="BLD-002", Name="Wooden Rainbow Stacker", Category="Building & Stacking", AgeRange="1-3 yrs", Price=24.99m, Stock=40, Description="12-piece wooden arch rainbow stacking toy" },
        new() { Id=3,  SKU="BLD-003", Name="Soft Foam Building Blocks Set", Category="Building & Stacking", AgeRange="6m-2 yrs", Price=17.99m, Stock=60, Description="40-piece lightweight foam blocks in bright colors" },
        new() { Id=4,  SKU="BLD-004", Name="Duplo Classic Brick Box", Category="Building & Stacking", AgeRange="1.5-5 yrs", Price=34.99m, Stock=35, Description="85 DUPLO bricks in a handy storage box" },
        new() { Id=5,  SKU="BLD-005", Name="Magnetic Tiles Starter Set", Category="Building & Stacking", AgeRange="3+ yrs", Price=39.99m, Stock=30, Description="32-piece magnetic building tiles for 3D creations" },
        new() { Id=6,  SKU="BLD-006", Name="Wooden Stacking Tower", Category="Building & Stacking", AgeRange="1-2 yrs", Price=14.99m, Stock=55, Description="5-ring classic wooden stacking tower with sorting base" },
        new() { Id=7,  SKU="BLD-007", Name="Bristle Blocks Creative Set", Category="Building & Stacking", AgeRange="2-5 yrs", Price=22.99m, Stock=45, Description="50-piece textured bristle blocks encourage creativity" },
        new() { Id=8,  SKU="BLD-008", Name="Giant Cardboard Blocks", Category="Building & Stacking", AgeRange="1-4 yrs", Price=29.99m, Stock=25, Description="Set of 20 oversized lightweight cardboard blocks" },
        new() { Id=9,  SKU="BLD-009", Name="Nesting & Stacking Cups", Category="Building & Stacking", AgeRange="6m-3 yrs", Price=9.99m,  Stock=80, Description="10-cup stacking set doubles as bath toys" },
        new() { Id=10, SKU="BLD-010", Name="Wooden Number Blocks", Category="Building & Stacking", AgeRange="2-4 yrs", Price=18.99m, Stock=50, Description="30 painted wooden blocks with numbers and letters" },

        // Stuffed Animals & Plush
        new() { Id=11, SKU="PLU-001", Name="Jellycat Bashful Bunny", Category="Stuffed Animals", AgeRange="0+ yrs", Price=22.99m, Stock=70, Description="Silky soft medium bunny, perfect snuggle companion" },
        new() { Id=12, SKU="PLU-002", Name="Aurora Miyoni Elephant Plush", Category="Stuffed Animals", AgeRange="0+ yrs", Price=16.99m, Stock=65, Description="Realistic plush elephant with floppy ears" },
        new() { Id=13, SKU="PLU-003", Name="Gund Baby Toothpick Bear", Category="Stuffed Animals", AgeRange="0+ yrs", Price=19.99m, Stock=55, Description="Ultra-soft teddy bear with embroidered face" },
        new() { Id=14, SKU="PLU-004", Name="Fisher-Price Soothe & Snuggle Otter", Category="Stuffed Animals", AgeRange="0-18m", Price=27.99m, Stock=40, Description="Plays lullabies and mimics breathing motion" },
        new() { Id=15, SKU="PLU-005", Name="Melissa & Doug Dinosaur Plush", Category="Stuffed Animals", AgeRange="2+ yrs", Price=14.99m, Stock=60, Description="Set of 3 soft dinosaur plush toys" },
        new() { Id=16, SKU="PLU-006", Name="Weighted Sensory Elephant", Category="Stuffed Animals", AgeRange="1+ yrs", Price=29.99m, Stock=30, Description="Calming weighted plush elephant for comfort" },
        new() { Id=17, SKU="PLU-007", Name="Baby Shark Plush Singing Doll", Category="Stuffed Animals", AgeRange="1+ yrs", Price=12.99m, Stock=75, Description="Sings the Baby Shark song with button press" },
        new() { Id=18, SKU="PLU-008", Name="Giraffe Rattle Plush", Category="Stuffed Animals", AgeRange="0-18m", Price=11.99m, Stock=80, Description="Crinkle and rattle giraffe for sensory play" },
        new() { Id=19, SKU="PLU-009", Name="Cozy Unicorn Stuffed Animal", Category="Stuffed Animals", AgeRange="1+ yrs", Price=17.99m, Stock=50, Description="Soft pastel unicorn with glittery horn" },
        new() { Id=20, SKU="PLU-010", Name="Manhattan Toy Wee Baby Stella", Category="Stuffed Animals", AgeRange="0+ yrs", Price=24.99m, Stock=45, Description="Soft-bodied baby doll with magnetic pacifier" },

        // Musical & Sound Toys
        new() { Id=21, SKU="MUS-001", Name="VTech Sit-to-Stand Learning Tower", Category="Musical Toys", AgeRange="9m-3 yrs", Price=44.99m, Stock=30, Description="Activity center with piano keys, phone, and songs" },
        new() { Id=22, SKU="MUS-002", Name="Wooden Xylophone", Category="Musical Toys", AgeRange="18m+ yrs", Price=13.99m, Stock=65, Description="8-note rainbow wooden xylophone with mallet" },
        new() { Id=23, SKU="MUS-003", Name="Fisher-Price Laugh & Learn Piano", Category="Musical Toys", AgeRange="6m-3 yrs", Price=24.99m, Stock=40, Description="Musical piano with letters, numbers, and sing-along songs" },
        new() { Id=24, SKU="MUS-004", Name="Mini Guitar for Toddlers", Category="Musical Toys", AgeRange="2+ yrs", Price=19.99m, Stock=35, Description="Acoustic 6-string toddler guitar with carrying strap" },
        new() { Id=25, SKU="MUS-005", Name="Drum Set for Kids", Category="Musical Toys", AgeRange="2-5 yrs", Price=34.99m, Stock=25, Description="5-piece toddler drum set with stool and sticks" },
        new() { Id=26, SKU="MUS-006", Name="Baby Einstein Take Along Tunes", Category="Musical Toys", AgeRange="3m-2 yrs", Price=9.99m,  Stock=90, Description="Portable music player with 7 classical melodies" },
        new() { Id=27, SKU="MUS-007", Name="Maracas & Shakers Set", Category="Musical Toys", AgeRange="1+ yrs", Price=11.99m, Stock=70, Description="Set of 6 colorful wooden shakers and maracas" },
        new() { Id=28, SKU="MUS-008", Name="LeapFrog Tapping Colors Drum", Category="Musical Toys", AgeRange="6m-3 yrs", Price=17.99m, Stock=50, Description="Drum teaches colors, animals, and music" },
        new() { Id=29, SKU="MUS-009", Name="Hape Pound & Tap Bench", Category="Musical Toys", AgeRange="12m-3 yrs", Price=22.99m, Stock=40, Description="Hammer bench with xylophone and wooden balls" },
        new() { Id=30, SKU="MUS-010", Name="Singing Microphone Toy", Category="Musical Toys", AgeRange="2+ yrs", Price=14.99m, Stock=55, Description="Echo microphone with flashing lights and music" },

        // Ride-On Toys
        new() { Id=31, SKU="RID-001", Name="Radio Flyer Classic Red Tricycle", Category="Ride-On Toys", AgeRange="2-4 yrs", Price=59.99m, Stock=20, Description="Classic steel trike with parent push handle" },
        new() { Id=32, SKU="RID-002", Name="Lil' Rider 3-Wheel Motorcycle", Category="Ride-On Toys", AgeRange="1-3 yrs", Price=44.99m, Stock=25, Description="Battery-powered ride-on motorcycle with sounds" },
        new() { Id=33, SKU="RID-003", Name="Wooden Balance Bike", Category="Ride-On Toys", AgeRange="18m-3 yrs", Price=49.99m, Stock=30, Description="Adjustable wooden balance bike with no pedals" },
        new() { Id=34, SKU="RID-004", Name="Bouncy Horse Rocker", Category="Ride-On Toys", AgeRange="1-4 yrs", Price=39.99m, Stock=20, Description="Spring horse rocker with sound effects and handles" },
        new() { Id=35, SKU="RID-005", Name="Plasma Car Ride-On", Category="Ride-On Toys", AgeRange="3+ yrs", Price=54.99m, Stock=15, Description="Self-powered car steered by wiggling the wheel" },
        new() { Id=36, SKU="RID-006", Name="Step2 Whisper Ride Buggy", Category="Ride-On Toys", AgeRange="1-3 yrs", Price=47.99m, Stock=18, Description="Toddler push buggy with parent handle and storage" },
        new() { Id=37, SKU="RID-007", Name="Fisher-Price Corn Popper Ride-On", Category="Ride-On Toys", AgeRange="12m-3 yrs", Price=29.99m, Stock=35, Description="Classic push toy with popping colored balls" },
        new() { Id=38, SKU="RID-008", Name="Mini Cooper Cozy Coupe", Category="Ride-On Toys", AgeRange="18m-5 yrs", Price=64.99m, Stock=12, Description="Iconic cozy coupe with working door and gas cap" },
        new() { Id=39, SKU="RID-009", Name="Skip Hop Zoo Scooter", Category="Ride-On Toys", AgeRange="2-4 yrs", Price=34.99m, Stock=28, Description="3-wheel scooter with light-up wheels and animal design" },
        new() { Id=40, SKU="RID-010", Name="Rocking Elephant Rider", Category="Ride-On Toys", AgeRange="1-3 yrs", Price=42.99m, Stock=22, Description="Plush elephant rocker with safety belt and sounds" },

        // Bath Toys
        new() { Id=41, SKU="BTH-001", Name="Munchkin Float & Play Bubbles", Category="Bath Toys", AgeRange="3m+ yrs", Price=8.99m,  Stock=90, Description="5 colorful floating bubble bath toys" },
        new() { Id=42, SKU="BTH-002", Name="Rubber Duck Collection", Category="Bath Toys", AgeRange="0+ yrs", Price=12.99m, Stock=80, Description="Set of 12 classic rubber ducks in assorted sizes" },
        new() { Id=43, SKU="BTH-003", Name="Boon Frog Pod Bath Toy Storage", Category="Bath Toys", AgeRange="6m+ yrs", Price=19.99m, Stock=50, Description="Frog-shaped bath toy organizer with scoop" },
        new() { Id=44, SKU="BTH-004", Name="Water Table Splash Pad", Category="Bath Toys", AgeRange="18m+ yrs", Price=27.99m, Stock=30, Description="Mini water table with spinning water features" },
        new() { Id=45, SKU="BTH-005", Name="Foam Bath Letters & Numbers", Category="Bath Toys", AgeRange="18m-5 yrs", Price=9.99m,  Stock=75, Description="36-piece foam alphabet and number bath set" },
        new() { Id=46, SKU="BTH-006", Name="Munchkin Shark Bath Squirt Toys", Category="Bath Toys", AgeRange="12m+ yrs", Price=11.99m, Stock=65, Description="Set of 8 squirting sea creature bath toys" },
        new() { Id=47, SKU="BTH-007", Name="Yookidoo Faucet Bath Toy", Category="Bath Toys", AgeRange="6m-3 yrs", Price=24.99m, Stock=40, Description="Clamp-on faucet extender with waterfall and cups" },
        new() { Id=48, SKU="BTH-008", Name="Bathtub Crayons Set", Category="Bath Toys", AgeRange="2+ yrs", Price=7.99m,  Stock=85, Description="10 washable bathtub crayons in bright colors" },
        new() { Id=49, SKU="BTH-009", Name="Stacking Bath Cups", Category="Bath Toys", AgeRange="6m-3 yrs", Price=6.99m,  Stock=95, Description="7-piece stacking and pouring bath cups" },
        new() { Id=50, SKU="BTH-010", Name="Bath Puppet Set", Category="Bath Toys", AgeRange="1-4 yrs", Price=14.99m, Stock=55, Description="4 waterproof animal hand puppets for bath play" },

        // Books & Learning
        new() { Id=51, SKU="BOK-001", Name="Pat the Bunny Cloth Book", Category="Books & Learning", AgeRange="0-2 yrs", Price=8.99m,  Stock=70, Description="Classic soft touch-and-feel activity book" },
        new() { Id=52, SKU="BOK-002", Name="Eric Carle Soft Book Set", Category="Books & Learning", AgeRange="0-3 yrs", Price=14.99m, Stock=60, Description="Set of 3 board books including Very Hungry Caterpillar" },
        new() { Id=53, SKU="BOK-003", Name="LeapFrog Interactive Farm Book", Category="Books & Learning", AgeRange="2-4 yrs", Price=19.99m, Stock=45, Description="Press-and-play sound book with farm animals and songs" },
        new() { Id=54, SKU="BOK-004", Name="Touch & Feel Animal Book", Category="Books & Learning", AgeRange="6m-2 yrs", Price=10.99m, Stock=65, Description="DK touch-and-feel book with soft texture patches" },
        new() { Id=55, SKU="BOK-005", Name="Alphabet Flashcard Set", Category="Books & Learning", AgeRange="2-5 yrs", Price=12.99m, Stock=55, Description="52 double-sided ABC flashcards with illustrations" },
        new() { Id=56, SKU="BOK-006", Name="Water Doodle Drawing Book", Category="Books & Learning", AgeRange="2+ yrs", Price=9.99m,  Stock=70, Description="Magic water drawing book with pen, mess-free" },
        new() { Id=57, SKU="BOK-007", Name="First 100 Words Board Book", Category="Books & Learning", AgeRange="6m-3 yrs", Price=7.99m,  Stock=80, Description="Bright photos teach first vocabulary words" },
        new() { Id=58, SKU="BOK-008", Name="Leap Frog Tag Junior Reader", Category="Books & Learning", AgeRange="2-4 yrs", Price=16.99m, Stock=40, Description="Interactive reading system with audio pen" },
        new() { Id=59, SKU="BOK-009", Name="Shapes & Colors Puzzle Book", Category="Books & Learning", AgeRange="18m-3 yrs", Price=11.99m, Stock=50, Description="Lift-the-flap book with shape and color lessons" },
        new() { Id=60, SKU="BOK-010", Name="Bilingual Spanish-English Board Book", Category="Books & Learning", AgeRange="6m-3 yrs", Price=9.99m,  Stock=60, Description="Introduces 100 words in English and Spanish" },

        // Art & Craft
        new() { Id=61, SKU="ART-001", Name="Crayola My First Crayons", Category="Art & Craft", AgeRange="18m+ yrs", Price=6.99m,  Stock=90, Description="16 washable large chunky crayons for toddlers" },
        new() { Id=62, SKU="ART-002", Name="Melissa & Doug Easel Set", Category="Art & Craft", AgeRange="3+ yrs", Price=54.99m, Stock=20, Description="Double-sided wooden easel with chalkboard and whiteboard" },
        new() { Id=63, SKU="ART-003", Name="Finger Paint Kit", Category="Art & Craft", AgeRange="18m+ yrs", Price=11.99m, Stock=65, Description="6-color washable finger paint set with apron" },
        new() { Id=64, SKU="ART-004", Name="Play-Doh Fun Factory Set", Category="Art & Craft", AgeRange="3+ yrs", Price=19.99m, Stock=50, Description="Factory machine with 10 cans of Play-Doh" },
        new() { Id=65, SKU="ART-005", Name="Sticker Activity Book", Category="Art & Craft", AgeRange="2+ yrs", Price=7.99m,  Stock=80, Description="500+ reusable sticker scenes for hours of play" },
        new() { Id=66, SKU="ART-006", Name="Kinetic Sand Starter Set", Category="Art & Craft", AgeRange="3+ yrs", Price=14.99m, Stock=45, Description="2lb kinetic sand with molds and tools" },
        new() { Id=67, SKU="ART-007", Name="Washable Dot Markers Set", Category="Art & Craft", AgeRange="2+ yrs", Price=12.99m, Stock=60, Description="8 washable bingo dauber markers with activity sheets" },
        new() { Id=68, SKU="ART-008", Name="Magnetic Drawing Board", Category="Art & Craft", AgeRange="18m+ yrs", Price=13.99m, Stock=55, Description="Mess-free magnetic doodle board with stamps" },
        new() { Id=69, SKU="ART-009", Name="Foam Sticker Craft Kit", Category="Art & Craft", AgeRange="2+ yrs", Price=8.99m,  Stock=70, Description="1000+ foam stickers for make-your-own art scenes" },
        new() { Id=70, SKU="ART-010", Name="Toddler Watercolor Set", Category="Art & Craft", AgeRange="3+ yrs", Price=9.99m,  Stock=65, Description="12-color washable watercolor palette with brush and paper" },

        // Puzzles & Shape Sorters
        new() { Id=71, SKU="PZL-001", Name="Melissa & Doug Wooden Shape Sorter", Category="Puzzles & Sorters", AgeRange="12m-3 yrs", Price=14.99m, Stock=60, Description="Shape-sorting cube with 30 colorful wooden shapes" },
        new() { Id=72, SKU="PZL-002", Name="Jumbo Knob Wooden Puzzle Set", Category="Puzzles & Sorters", AgeRange="18m-3 yrs", Price=16.99m, Stock=55, Description="Set of 3 puzzles: farm, vehicles, and animals" },
        new() { Id=73, SKU="PZL-003", Name="Floor Puzzle Alphabet", Category="Puzzles & Sorters", AgeRange="3-5 yrs", Price=12.99m, Stock=45, Description="26-piece giant floor puzzle featuring ABCs" },
        new() { Id=74, SKU="PZL-004", Name="VTech Sort & Discover Activity Cube", Category="Puzzles & Sorters", AgeRange="12m-3 yrs", Price=22.99m, Stock=40, Description="Electronic shape sorter that teaches shapes and colors" },
        new() { Id=75, SKU="PZL-005", Name="Melissa & Doug Safari Floor Puzzle", Category="Puzzles & Sorters", AgeRange="2-5 yrs", Price=9.99m,  Stock=50, Description="30-piece extra-thick floor puzzle with safari animals" },
        new() { Id=76, SKU="PZL-006", Name="Magnetic Farm Puzzle", Category="Puzzles & Sorters", AgeRange="2-5 yrs", Price=11.99m, Stock=55, Description="20-piece magnetic puzzle playboard with farm scene" },
        new() { Id=77, SKU="PZL-007", Name="Wooden Vehicle Chunky Puzzle", Category="Puzzles & Sorters", AgeRange="12m-3 yrs", Price=13.99m, Stock=60, Description="8-piece chunky peg puzzle with colorful vehicles" },
        new() { Id=78, SKU="PZL-008", Name="Color & Shape Sorting Board", Category="Puzzles & Sorters", AgeRange="2-4 yrs", Price=17.99m, Stock=45, Description="Wooden board with 37 colored shape pieces" },
        new() { Id=79, SKU="PZL-009", Name="Number Puzzle Train", Category="Puzzles & Sorters", AgeRange="2-5 yrs", Price=19.99m, Stock=40, Description="Pull-along train that doubles as a 10-piece number puzzle" },
        new() { Id=80, SKU="PZL-010", Name="Foam Interlocking Floor Tiles", Category="Puzzles & Sorters", AgeRange="0+ yrs", Price=24.99m, Stock=35, Description="36-piece ABC foam mat tiles for safe play area" },

        // Push & Pull Toys
        new() { Id=81, SKU="PSH-001", Name="Melissa & Doug Pound & Roll Tower", Category="Push & Pull Toys", AgeRange="12m-3 yrs", Price=22.99m, Stock=45, Description="Wooden pounding tower with hammer and balls" },
        new() { Id=82, SKU="PSH-002", Name="Classic Corn Popper Push Toy", Category="Push & Pull Toys", AgeRange="12m-3 yrs", Price=14.99m, Stock=60, Description="Iconic push toy with popping rainbow balls" },
        new() { Id=83, SKU="PSH-003", Name="Wooden Pull-Along Duck", Category="Push & Pull Toys", AgeRange="12m-3 yrs", Price=16.99m, Stock=55, Description="Quacking wooden duck on pull string" },
        new() { Id=84, SKU="PSH-004", Name="Activity Walker Cube", Category="Push & Pull Toys", AgeRange="9m-2 yrs", Price=34.99m, Stock=30, Description="5-sided activity cube with push handle for walkers" },
        new() { Id=85, SKU="PSH-005", Name="VTech Push & Ride Alphabet Train", Category="Push & Pull Toys", AgeRange="12m-3 yrs", Price=27.99m, Stock=35, Description="Ride-on and push train that teaches letters and numbers" },
        new() { Id=86, SKU="PSH-006", Name="Pull-Along Snail Toy", Category="Push & Pull Toys", AgeRange="18m-3 yrs", Price=12.99m, Stock=65, Description="Colorful wooden snail with clicking shell on pull string" },
        new() { Id=87, SKU="PSH-007", Name="Mega Bloks First Builders Wagon", Category="Push & Pull Toys", AgeRange="12m-4 yrs", Price=29.99m, Stock=28, Description="Push wagon loaded with 50 big first builder blocks" },
        new() { Id=88, SKU="PSH-008", Name="Wooden Bead Maze Push Toy", Category="Push & Pull Toys", AgeRange="12m-3 yrs", Price=19.99m, Stock=40, Description="Rolling toy with bead maze top for fine motor skills" },
        new() { Id=89, SKU="PSH-009", Name="Musical Elephant Walker", Category="Push & Pull Toys", AgeRange="9m-2 yrs", Price=24.99m, Stock=35, Description="Elephant push walker plays music while baby walks" },
        new() { Id=90, SKU="PSH-010", Name="Butterfly Push Along Toy", Category="Push & Pull Toys", AgeRange="12m-3 yrs", Price=18.99m, Stock=50, Description="Spinning butterfly wings pop-up toy on push handle" },

        // Outdoor & Active Play
        new() { Id=91,  SKU="OUT-001", Name="Little Tikes Totsports T-Ball Set", Category="Outdoor & Active", AgeRange="18m-4 yrs", Price=17.99m, Stock=50, Description="Adjustable height t-ball set with bat and 6 balls" },
        new() { Id=92,  SKU="OUT-002", Name="Sprinkler Splash Pad", Category="Outdoor & Active", AgeRange="1+ yrs", Price=22.99m, Stock=40, Description="60-inch outdoor splash pad with sprinkler jets" },
        new() { Id=93,  SKU="OUT-003", Name="Sand & Water Table", Category="Outdoor & Active", AgeRange="2-6 yrs", Price=49.99m, Stock=20, Description="Two-sided table with sand and water compartments" },
        new() { Id=94,  SKU="OUT-004", Name="Step2 Naturally Playful Sandbox", Category="Outdoor & Active", AgeRange="2+ yrs", Price=44.99m, Stock=15, Description="Sandbox with canopy cover and mold-resistant material" },
        new() { Id=95,  SKU="OUT-005", Name="Little Tikes Easy Score Basketball", Category="Outdoor & Active", AgeRange="18m-5 yrs", Price=27.99m, Stock=35, Description="Adjustable height basketball hoop set for toddlers" },
        new() { Id=96,  SKU="OUT-006", Name="Toddler Bubble Machine", Category="Outdoor & Active", AgeRange="18m+ yrs", Price=14.99m, Stock=60, Description="Automatic bubble machine makes 500+ bubbles per minute" },
        new() { Id=97,  SKU="OUT-007", Name="Gardening Tool Set for Kids", Category="Outdoor & Active", AgeRange="3+ yrs", Price=16.99m, Stock=50, Description="5-piece real wood gardening tool set with tote bag" },
        new() { Id=98,  SKU="OUT-008", Name="First Bike Helmet & Pad Set", Category="Outdoor & Active", AgeRange="2-5 yrs", Price=34.99m, Stock=25, Description="Toddler helmet with matching knee and elbow pads" },
        new() { Id=99,  SKU="OUT-009", Name="Pop-Up Play Tent", Category="Outdoor & Active", AgeRange="2+ yrs", Price=29.99m, Stock=30, Description="Instant pop-up tunnel and tent combo for indoor/outdoor" },
        new() { Id=100, SKU="OUT-010", Name="Swing Set Toddler Bucket Seat", Category="Outdoor & Active", AgeRange="6m-3 yrs", Price=32.99m, Stock=22, Description="Safety toddler bucket swing with T-bar and rope" },
    };
}
