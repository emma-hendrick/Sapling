method t = {
    printf("Enter 'a' for great success in life: ");
    char c = getchar();
    flush_stdin();
    printf(c == 'a' ? "WOOHOOO" : "DANG");
    return c == 'a' ? 0 : 1;
}

method s = {
    return 1;
}

# If at least one method returns 0, return 0
return t() == 0 || s() == 0 ? 0 : 1;