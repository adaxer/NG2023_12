using Microsoft.EntityFrameworkCore;
using MovieBase.Common;
using System.Reflection.Metadata;

namespace MovieBase.Data;

public class MovieContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Award> Awards { get; set; }
    public DbSet<Merchandising> Merchandisings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>()
            .HasMany(e => e.Merchandisings)
            .WithOne(e => e.Movie)
            .HasForeignKey(e => e.MovieId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);

        modelBuilder.Entity<Movie>()
            .HasMany(e => e.Awards)
            .WithMany(e => e.Movies)
            .UsingEntity(
                l => l.HasOne(typeof(Award)).WithMany().OnDelete(DeleteBehavior.Cascade),
                r => r.HasOne(typeof(Movie)).WithMany().OnDelete(DeleteBehavior.Cascade));
    }

    public MovieContext(DbContextOptions<MovieContext> options) : base(options)
    {
        Seed();
    }

    void Seed()
    {
        Database.EnsureCreated();
        if (!Movies.Any()) // Überprüft, ob bereits Filme vorhanden sind
        {
            var movies = new List<Movie>
            {
                new Movie { Id = 1, Title = "Blade Runner", Director = "James Cameron", Released = new DateOnly(1982, 6, 25) },
                new Movie { Id = 2, Title = "Star Wars: Episode IV - A New Hope", Director = "George Lucas", Released = new DateOnly(1977, 5, 25) },
                new Movie { Id = 3, Title = "The Matrix", Director = "Lana Wachowski, Lilly Wachowski", Released = new DateOnly(1999, 3, 31) },
                new Movie { Id = 4, Title = "Inception", Director = "Christopher Nolan", Released = new DateOnly(2010, 7, 16) },
                new Movie { Id = 5, Title = "2001: A Space Odyssey", Director = "Stanley Kubrick", Released = new DateOnly(1968, 4, 6) },
                new Movie { Id = 6, Title = "Interstellar", Director = "Christopher Nolan", Released = new DateOnly(2014, 11, 7) },
                new Movie { Id = 7, Title = "Guardians of the Galaxy", Director = "James Gunn", Released = new DateOnly(2014, 8, 1) },
                new Movie { Id = 8, Title = "Avatar", Director = "James Cameron", Released = new DateOnly(2009, 12, 18) },
                new Movie { Id = 9, Title = "Arrival", Director = "Denis Villeneuve", Released = new DateOnly(2016, 11, 11) },
                new Movie { Id = 10, Title = "Ender's Game", Director = "Gavin Hood", Released = new DateOnly(2013, 11, 1) },
                new Movie { Id = 11, Title = "The Day the Earth Stood Still", Director = "Robert Wise", Released = new DateOnly(1951, 9, 18) },
                new Movie { Id = 12, Title = "Metropolis", Director = "Fritz Lang", Released = new DateOnly(1927, 1, 10) },
                new Movie { Id = 13, Title = "A Clockwork Orange", Director = "Stanley Kubrick", Released = new DateOnly(1971, 12, 19) },
                new Movie { Id = 14, Title = "Solaris", Director = "Andrei Tarkovsky", Released = new DateOnly(1972, 3, 20) },
                new Movie { Id = 15, Title = "Dune", Director = "David Lynch", Released = new DateOnly(1984, 12, 14) },
                new Movie { Id = 16, Title = "Star Trek: The Wrath of Khan", Director = "Nicholas Meyer", Released = new DateOnly(1982, 6, 4) },
                new Movie { Id = 17, Title = "Star Wars: Episode V - The Empire Strikes Back", Director = "Irvin Kershner", Released = new DateOnly(1980, 5, 21) },
                new Movie { Id = 18, Title = "War of the Worlds", Director = "Byron Haskin", Released = new DateOnly(1953, 8, 13) },
                new Movie { Id = 19, Title = "Moon", Director = "Duncan Jones", Released = new DateOnly(2009, 6, 12) },
                new Movie { Id = 20, Title = "Children of Men", Director = "Alfonso Cuarón", Released = new DateOnly(2006, 9, 22) },
                new Movie { Id = 21, Title = "The Andromeda Strain", Director = "Robert Wise", Released = new DateOnly(1971, 3, 12) },
                new Movie { Id = 22, Title = "Close Encounters of the Third Kind", Director = "Steven Spielberg", Released = new DateOnly(1977, 11, 16) },
                new Movie { Id = 23, Title = "Alien", Director = "Ridley Scott", Released = new DateOnly(1979, 5, 25) },
                new Movie { Id = 24, Title = "The Terminator", Director = "James Cameron", Released = new DateOnly(1984, 10, 26) },
                new Movie { Id = 25, Title = "Back to the Future", Director = "Robert Zemeckis", Released = new DateOnly(1985, 7, 3) },
                new Movie { Id = 26, Title = "RoboCop", Director = "Paul Verhoeven", Released = new DateOnly(1987, 7, 17) },
                new Movie { Id = 27, Title = "Predator", Director = "John McTiernan", Released = new DateOnly(1987, 6, 12) },
                new Movie { Id = 28, Title = "Total Recall", Director = "Paul Verhoeven", Released = new DateOnly(1990, 6, 1) },
                new Movie { Id = 29, Title = "The Fifth Element", Director = "Luc Besson", Released = new DateOnly(1997, 5, 9) },
                new Movie { Id = 30, Title = "Gattaca", Director = "Andrew Niccol", Released = new DateOnly(1997, 10, 24) },
                new Movie { Id = 31, Title = "12 Monkeys", Director = "Terry Gilliam", Released = new DateOnly(1995, 12, 29) },
                new Movie { Id = 32, Title = "The Minority Report", Director = "Steven Spielberg", Released = new DateOnly(2002, 6, 21) },
                new Movie { Id = 33, Title = "Eternal Sunshine of the Spotless Mind", Director = "Michel Gondry", Released = new DateOnly(2004, 3, 19) },
                new Movie { Id = 34, Title = "District 9", Director = "Neill Blomkamp", Released = new DateOnly(2009, 8, 14) },
                new Movie { Id = 35, Title = "Ex Machina", Director = "Alex Garland", Released = new DateOnly(2015, 1, 21) },
                new Movie { Id = 36, Title = "Looper", Director = "Rian Johnson", Released = new DateOnly(2012, 9, 28) },
                new Movie { Id = 37, Title = "Her", Director = "Spike Jonze", Released = new DateOnly(2013, 12, 18) },
                new Movie { Id = 38, Title = "The Martian", Director = "Ridley Scott", Released = new DateOnly(2015, 9, 30) },
                new Movie { Id = 39, Title = "Blade Runner 2049", Director = "Denis Villeneuve", Released = new DateOnly(2017, 10, 6) },
                new Movie { Id = 40, Title = "Ready Player One", Director = "Steven Spielberg", Released = new DateOnly(2018, 3, 29) },
                new Movie { Id = 41, Title = "Altered States", Director = "Ken Russell", Released = new DateOnly(1980, 12, 25) },
                new Movie { Id = 42, Title = "Starship Troopers", Director = "Paul Verhoeven", Released = new DateOnly(1997, 11, 7) },
                new Movie { Id = 43, Title = "Dark City", Director = "Alex Proyas", Released = new DateOnly(1998, 2, 27) },
                new Movie { Id = 44, Title = "Donnie Darko", Director = "Richard Kelly", Released = new DateOnly(2001, 1, 19) },
                new Movie { Id = 45, Title = "A.I. Artificial Intelligence", Director = "Steven Spielberg", Released = new DateOnly(2001, 6, 29) },
                new Movie { Id = 46, Title = "Moon", Director = "Duncan Jones", Released = new DateOnly(2009, 6, 12) },
                new Movie { Id = 47, Title = "Avatar", Director = "James Cameron", Released = new DateOnly(2009, 12, 18) },
                new Movie { Id = 48, Title = "Oblivion", Director = "Joseph Kosinski", Released = new DateOnly(2013, 4, 19) },
                new Movie { Id = 49, Title = "Gravity", Director = "Alfonso Cuarón", Released = new DateOnly(2013, 10, 4) },
                new Movie { Id = 50, Title = "Edge of Tomorrow", Director = "Doug Liman", Released = new DateOnly(2014, 5, 28) },
                new Movie { Id = 51, Title = "The Road", Director = "John Hillcoat", Released = new DateOnly(2009, 11, 25) },
                new Movie { Id = 52, Title = "Snowpiercer", Director = "Bong Joon-ho", Released = new DateOnly(2013, 8, 1) },
                new Movie { Id = 53, Title = "The Time Machine", Director = "George Pal", Released = new DateOnly(1960, 8, 17) },
                new Movie { Id = 54, Title = "Stalker", Director = "Andrei Tarkovsky", Released = new DateOnly(1979, 5, 26) },
                new Movie { Id = 55, Title = "The War of the Worlds", Director = "Byron Haskin", Released = new DateOnly(1953, 8, 13) },
                new Movie { Id = 56, Title = "Invasion of the Body Snatchers", Director = "Don Siegel", Released = new DateOnly(1956, 2, 5) },
                new Movie { Id = 57, Title = "Brazil", Director = "Terry Gilliam", Released = new DateOnly(1985, 12, 13) },
                new Movie { Id = 58, Title = "The Adjustment Bureau", Director = "George Nolfi", Released = new DateOnly(2011, 3, 4) },
                new Movie { Id = 59, Title = "Prometheus", Director = "Ridley Scott", Released = new DateOnly(2012, 6, 1) },
                new Movie { Id = 60, Title = "Star Wars: Episode VI - Return of the Jedi", Director = "Richard Marquand", Released = new DateOnly(1983, 5, 25) },
                new Movie { Id = 61, Title = "Contact", Director = "Robert Zemeckis", Released = new DateOnly(1997, 7, 11) },
                new Movie { Id = 62, Title = "Wall-E", Director = "Andrew Stanton", Released = new DateOnly(2008, 6, 27) },
                new Movie { Id = 63, Title = "I Am Legend", Director = "Francis Lawrence", Released = new DateOnly(2007, 12, 14) },
                new Movie { Id = 64, Title = "Serenity", Director = "Joss Whedon", Released = new DateOnly(2005, 9, 30) },
                new Movie { Id = 65, Title = "The Chronicles of Riddick", Director = "David Twohy", Released = new DateOnly(2004, 6, 11) },
                new Movie { Id = 66, Title = "I, Robot", Director = "Alex Proyas", Released = new DateOnly(2004, 7, 16) },
                new Movie { Id = 67, Title = "The Abyss", Director = "James Cameron", Released = new DateOnly(1989, 8, 9) },
                new Movie { Id = 68, Title = "Galaxy Quest", Director = "Dean Parisot", Released = new DateOnly(1999, 12, 23) },
                new Movie { Id = 69, Title = "Planet of the Apes", Director = "Franklin J. Schaffner", Released = new DateOnly(1968, 4, 3) },
                new Movie { Id = 70, Title = "Logan's Run", Director = "Michael Anderson", Released = new DateOnly(1976, 6, 23) },
                new Movie { Id = 71, Title = "The Fountain", Director = "Darren Aronofsky", Released = new DateOnly(2006, 9, 4) },
                new Movie { Id = 72, Title = "TRON", Director = "Steven Lisberger", Released = new DateOnly(1982, 7, 9) },
                new Movie { Id = 73, Title = "Silent Running", Director = "Douglas Trumbull", Released = new DateOnly(1972, 3, 10) },
                new Movie { Id = 74, Title = "Starman", Director = "John Carpenter", Released = new DateOnly(1984, 12, 14) },
                new Movie { Id = 75, Title = "Equilibrium", Director = "Kurt Wimmer", Released = new DateOnly(2002, 12, 6) },
                new Movie { Id = 76, Title = "The Thirteenth Floor", Director = "Josef Rusnak", Released = new DateOnly(1999, 4, 16) },
                new Movie { Id = 77, Title = "The Thing", Director = "John Carpenter", Released = new DateOnly(1982, 6, 25) },
                new Movie { Id = 78, Title = "V for Vendetta", Director = "James McTeigue", Released = new DateOnly(2005, 12, 11) },
                new Movie { Id = 79, Title = "Arrival", Director = "Denis Villeneuve", Released = new DateOnly(2016, 11, 11) },
                new Movie { Id = 80, Title = "The Lobster", Director = "Yorgos Lanthimos", Released = new DateOnly(2015, 5, 15) },
                new Movie { Id = 81, Title = "Event Horizon", Director = "Paul W.S. Anderson", Released = new DateOnly(1997, 8, 15) },
                new Movie { Id = 82, Title = "The Island", Director = "Michael Bay", Released = new DateOnly(2005, 7, 22) },
                new Movie { Id = 83, Title = "Coherence", Director = "James Ward Byrkit", Released = new DateOnly(2013, 9, 19) },
                new Movie { Id = 84, Title = "Annihilation", Director = "Alex Garland", Released = new DateOnly(2018, 2, 23) },
                new Movie { Id = 85, Title = "THX 1138", Director = "George Lucas", Released = new DateOnly(1971, 3, 11) },
                new Movie { Id = 86, Title = "Solaris", Director = "Steven Soderbergh", Released = new DateOnly(2002, 11, 29) },
                new Movie { Id = 87, Title = "Source Code", Director = "Duncan Jones", Released = new DateOnly(2011, 4, 1) },
                new Movie { Id = 88, Title = "Under the Skin", Director = "Jonathan Glazer", Released = new DateOnly(2013, 9, 29) },
                new Movie { Id = 89, Title = "Colossal", Director = "Nacho Vigalondo", Released = new DateOnly(2016, 9, 9) },
                new Movie { Id = 90, Title = "Another Earth", Director = "Mike Cahill", Released = new DateOnly(2011, 7, 22) },
                new Movie { Id = 91, Title = "Sunshine", Director = "Danny Boyle", Released = new DateOnly(2007, 4, 5) },
                new Movie { Id = 92, Title = "Pandorum", Director = "Christian Alvart", Released = new DateOnly(2009, 9, 25) },
                new Movie { Id = 93, Title = "Ghost in the Shell", Director = "Mamoru Oshii", Released = new DateOnly(1995, 11, 18) },
                new Movie { Id = 94, Title = "The Day the Earth Stood Still", Director = "Robert Wise", Released = new DateOnly(1951, 9, 18) },
                new Movie { Id = 95, Title = "Gattaca", Director = "Andrew Niccol", Released = new DateOnly(1997, 10, 24) },
                new Movie { Id = 96, Title = "The Andromeda Strain", Director = "Robert Wise", Released = new DateOnly(1971, 3, 12) },
                new Movie { Id = 97, Title = "The 5th Wave", Director = "J Blakeson", Released = new DateOnly(2016, 1, 22) },
                new Movie { Id = 98, Title = "Sphere", Director = "Barry Levinson", Released = new DateOnly(1998, 2, 13) },
                new Movie { Id = 99, Title = "War of the Worlds", Director = "Steven Spielberg", Released = new DateOnly(2005, 6, 29) },
                new Movie { Id = 100, Title = "Predestination", Director = "Michael Spierig & Peter Spierig", Released = new DateOnly(2014, 8, 28) },
           };
            var awards = new List<Award> {
                new Award { Id = 1, Name = "Oskar", Movies = new List<Movie> { movies.First(), movies.Skip(5).First() } },
                new Award { Id = 2, Name = "Golden Globe", Movies = new List<Movie> { movies.Skip(1).First(), movies.Skip(5).First() } },
                new Award { Id = 3, Name = "Bayer. Filmpreis", Movies = new List<Movie> { movies.First(), movies.Skip(6).First() } },
            };

            Movies.AddRange(movies);
            Awards.AddRange(awards);
            SaveChanges();
        }
    }
}
