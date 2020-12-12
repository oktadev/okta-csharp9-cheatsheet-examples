namespace Cs9CheatSheet.DefaultInterfaceMethods.NonBreakingInterfaceAugmentation
{
    //Library Version 1
    namespace Version1
    {
        namespace Library
        {
            interface ILibraryInterface
            {
                int Increment(int i);
            }
        }

        namespace Application
        {
            using Library;
            class UserClass : ILibraryInterface
            {
                public int Increment(int i) => i + 1;
            }
        }
    }

    //Library Version 2: Add Decrement method to interface
    //This change is breaking, because it forces the user to modify her code,
    //and add an implementation for the added method
    namespace Version2_Ideal
    {
        namespace Library
        {
            interface ILibraryInterface
            {
                int Increment(int i);
                int Decrement(int i);
            }
        }

        namespace Application
        {
            using Library;
            //class UserClass : ILibraryInterface
            //{
            //    public int Increment(int i) => i + 1;
            //}
            class UserClass : ILibraryInterface
            {
                public int Increment(int i) => i + 1;
                public int Decrement(int i) => i - 1;

            }
        }
    }

    //Library Version 2 with additional interface
    //This change is not breaking, but it imposes the creation of new interface
    //with consequent naming pollution and messy architecture
    namespace Version2_Additional_Interface
    {
        namespace Library
        {
            interface ILibraryInterface
            {
                int Increment(int i);
            }
            interface ILibraryInterface2 : ILibraryInterface
            {
                int Decrement(int i);
            }
        }

        namespace Application
        {
            using Library;
            class UserClass : ILibraryInterface
            {
                public int Increment(int i) => i + 1;
            }
        }
    }

    //Library Version 2 with default implementation (C#8)
    //This change is not breaking, and avoids the issues above
    namespace Version2_Default_Implementation
    {
        namespace Library
        {
            interface ILibraryInterface
            {
                int Increment(int i);
                int Decrement(int i) => i - 1;
            }
        }

        namespace Application
        {
            using Library;
            class UserClass : ILibraryInterface
            {
                public int Increment(int i) => i + 1;
            }
        }
    }
}
