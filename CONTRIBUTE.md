# Coding Standards

These are the standards our team has come to agreement with this project.

## Coding styles

1) Usage of `var`
 - Can I, in general, using common sense, use them?
	- Expected: Yes, they are used in the Core code when type is inferred and/or obvious.
 - When is it OK to do it?
	- For built-in types? (Example: `var i = 5;`)
		- YES
	- When a variable type is apparent? (Example: `var c = new C();`)
		- YES
	- Elsewhere? (I expect not.)
		- Not recommended tbh.
		
2) Line endings
	- CR/LF (Windows Style)

3) Tabs or spaces?
	- Spaces. (forced by VisualStudio anyway)

4) Usage of `this.` ?
	- Nope. Only when necessary for certain functionalities of C#.

## Design laws

1) Use composition instead of inheritance
	- Classes should achieve polymorphic behavior and code reuse by their composition (by containing instances of other classes that implement the desired functionality) rather than inheritance from a base or parent class.
2) Classes should be smaller than 150 lines.
3) Methods with functionality should be smaller than 10 lines.