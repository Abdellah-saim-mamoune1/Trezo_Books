import { Link, useNavigate } from "react-router-dom";
import { useEffect, useState, useRef } from "react";
import { useAppDispatch, useAppSelector } from "../../Slices/Hooks.ts";
import { GetBooksTypes } from "../../APIs/GetBooksCopiesAPIs.ts";
import { FiShoppingCart } from "react-icons/fi";
import { SetLoggedInState } from "../../Slices/ClientSlices/ClientInfoSlice.ts";
import { DeleteCookiesAPI, GetCartAPI, GetClientInfoAPI } from "../../APIs/ClientAPIs.ts";
import { HiMenuAlt3, HiX } from "react-icons/hi";
import { ClearCart } from "../../Slices/ClientSlices/CartSlice.ts";

export default function TopNav() {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const [showCategories, setShowCategories] = useState(false);
  const [dropdownOpen, setDropdownOpen] = useState(false);
  const [burgerOpen, setBurgerOpen] = useState(false);
  const [searchTerm, setSearchTerm] = useState("");

  const categoryRef = useRef<HTMLDivElement>(null);
  const dropdownRef = useRef<HTMLDivElement>(null);

  const categories = useAppSelector((state) => state.PublicSlice.BooksTypes);
  const cartQuantity = useAppSelector((state) => state.CartSlice.Quantity);
  const userName = useAppSelector((state) => state.ClientInfoSlice.ClientInfo?.firstName);
  const isLoggedIn = useAppSelector((state) => state.ClientInfoSlice.IsLoogedIn);

  useEffect(() => {
    dispatch(GetBooksTypes());
  }, []);

  useEffect(() => {
    if (isLoggedIn) {
      dispatch(GetClientInfoAPI());
      dispatch(GetCartAPI());
    }
  }, [isLoggedIn]);

  useEffect(() => {
    function handleClickOutside(event: MouseEvent) {
      if (
        categoryRef.current &&
        !categoryRef.current.contains(event.target as Node)
      ) {
        setShowCategories(false);
      }
      if (
        dropdownRef.current &&
        !dropdownRef.current.contains(event.target as Node)
      ) {
        setDropdownOpen(false);
      }
    }

    document.addEventListener("mousedown", handleClickOutside);
    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, []);

  const handleSignOut = () => {
    dispatch(ClearCart());
    dispatch(SetLoggedInState(false));
    DeleteCookiesAPI();
    navigate("/");
  };

  const handleSearchSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    if (!searchTerm.trim()) return;
    localStorage.setItem("searchQuery", searchTerm.trim());
    navigate("/searchresult");
  };

  return (
    <nav className="sticky top-0 z-50 bg-white border-b border-gray-200 px-4 sm:px-6 py-4 shadow-sm text-gray-800 ">
      <div className="flex items-center justify-between">
       
        <div className="flex items-center space-x-4">
          <Link to="/" className="text-2xl font-bold">
            Trezo
          </Link>

          <button
            className="sm:hidden text-2xl"
            onClick={() => setBurgerOpen(!burgerOpen)}
          >
            {burgerOpen ? <HiX /> : <HiMenuAlt3 />}
          </button>

     
          <div className="hidden sm:block relative" ref={categoryRef}>
            <button
              onClick={() => setShowCategories(!showCategories)}
              className="text-sm font-medium text-gray-700  hover:text-black "
            >
              Categories ▼
            </button>

            {showCategories && (
              <div className="absolute mt-2 bg-white border border-gray-200 rounded shadow-md z-20 p-4 w-48 max-h-64 overflow-auto flex flex-col space-y-2">
                {categories?.map((type, index) => (
                  <button
                    key={index}
                    onClick={() => {
                      setShowCategories(false);
                      navigate("/" + type.name);
                    }}
                    className="text-sm hover:bg-gray-100 px-3 py-1 rounded text-left"
                  >
                    {type.name}
                  </button>
                ))}
              </div>
            )}
          </div>
        </div>

     
        <form onSubmit={handleSearchSubmit} className="hidden sm:flex flex-1 mx-6">
          <input
            type="text"
            placeholder="Search books..."
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
            className="w-full rounded-lg border border-gray-300 px-4 py-2 text-sm bg-white text-gray-800 focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
        </form>

      
        <div className="flex items-center space-x-4 relative" ref={dropdownRef}>
        
          <div className="relative">
            <button
              onClick={() => setDropdownOpen(!dropdownOpen)}
              className="text-sm font-medium"
            >
              {isLoggedIn && userName ? `${userName} ▼` : "Sign In ▼"}
            </button>

            {dropdownOpen && (
              <div className="absolute right-0 mt-2 bg-white border border-gray-200 rounded shadow-md z-30 w-40 text-sm">
                {isLoggedIn ? (
                  <>
                    <button onClick={() => { navigate("/account"); setDropdownOpen(false); }} className="block w-full text-left px-4 py-2 hover:bg-gray-100">Account</button>
                    <button onClick={() => { navigate("/orders"); setDropdownOpen(false); }} className="block w-full text-left px-4 py-2 hover:bg-gray-100 ">Orders</button>
                  
                    <button onClick={handleSignOut} className="block w-full text-left px-4 py-2 text-red-600 hover:bg-red-100 ">Sign Out</button>
                  </>
                ) : (
                  <button onClick={() => { navigate("/login"); setDropdownOpen(false); }} className="block w-full text-left px-4 py-2 hover:bg-gray-100">Go to Login</button>
                )}
              </div>
            )}
          </div>

         
          <Link to="/cart" className="relative">
            <FiShoppingCart className="text-2xl" />
            {cartQuantity > 0 && (
              <span className="absolute -top-2 -right-2 bg-red-600 text-white text-xs font-bold px-1.5 py-0.5 rounded-full">
                {cartQuantity}
              </span>
            )}
          </Link>
        </div>
      </div>

     
      {burgerOpen && (
        <div className="sm:hidden mt-4 space-y-4">
         
          <div className="bg-white border rounded p-4 space-y-2">
            <p className="font-semibold mb-2">Categories</p>
            {categories?.map((type, index) => (
              <button
                key={index}
                onClick={() => {
                  setBurgerOpen(false);
                  navigate("/" + type.name);
                }}
                className="block text-sm w-full text-left px-2 py-1 hover:bg-gray-100 rounded"
              >
                {type.name}
              </button>
            ))}
          </div>

        
          <form onSubmit={handleSearchSubmit} className="px-4">
            <input
              type="text"
              placeholder="Search books..."
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
              className="w-full rounded-lg border border-gray-300 px-4 py-2 text-sm bg-white text-gray-800 focus:outline-none focus:ring-2 focus:ring-blue-500"
            />
          </form>
        </div>
      )}
    </nav>
  );
}
