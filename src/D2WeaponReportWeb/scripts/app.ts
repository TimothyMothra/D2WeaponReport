function sayHello() {
    const testInput = (document.getElementById("typescript_test_input") as HTMLInputElement).value;
    return `Hello from ${testInput}!`;
}
