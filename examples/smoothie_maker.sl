# Ask user if smoooothie
printf("Hello, would you like a smoothie? (y/n): ");

# Give / no give smooooothie
char c1 = getchar();
flush_stdin();
str s1 = c1 == 'y' ? "Here ya go! Say thanks? (y/n): " : "Awwww... Too bad. Say thank you anyways? (y/n): ";
printf(s1);

# Give / no give good day
char c2 = getchar();
flush_stdin();
str s2 = c2 == 'y' ? "You're welcome! Have a great day!!! " : "That's mean... Have a bad day... ";
printf(s2);

# Return success if they say thanks
return c2 == 'y' ? 0 : -1;