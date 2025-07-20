import { useEffect, useState } from "react";
import { useAppDispatch, useAppSelector } from "../../Slices/Hooks.ts";
import {
  GetBestSellersgBooks,
  GetBookInfo,
  GetNewReleasedBooks,
  GetRecommendedBooks,
  GetTopRatedBooks
} from "../../APIs/GetBooksCopiesAPIs.ts";
import { SetBookIdToLocalStorage } from "../../Utilities/UClient.ts";
import { useNavigate } from "react-router-dom";
import { Book, GetProductInfo } from "../../Interfaces/PublicInterfaces.ts";
import { CartItem,AddToCart } from "../../Slices/ClientSlices/CartSlice.ts";
import { AddProductToCartAPI, GetCartAPI } from "../../APIs/ClientAPIs.ts";
import { SetLoggedInState } from "../../Slices/ClientSlices/ClientInfoSlice.ts";
import { SlideAlert } from "../Product/SlideAlertProps.tsx";

export function Home() {
  const navigate = useNavigate();
  const dispatch = useAppDispatch();
  const [showAddToCartScreen, setShowAddToCartScreen] = useState(false);
  const [alert, setAlert] = useState<{ type: "success" | "error"; message: string } | null>(null);

  const RecommendedBooks = useAppSelector((state) => state.PublicSlice.RecommendedBooks);
  const BestSellersBooks = useAppSelector((state) => state.PublicSlice.BestSellersBooks);
  const NewBooks = useAppSelector((state) => state.PublicSlice.NewReleasedBooks);
  const TopRatedBooks = useAppSelector((state) => state.PublicSlice.TopRatedBooks);
  const Product = useAppSelector((state) => state.PublicSlice.GetProductInfo);
  const IsLoggedIn = useAppSelector((state) => state.ClientInfoSlice.IsLoogedIn);
  const CartItems=useAppSelector(s=>s.CartSlice.Items);
  useEffect(() => {
    dispatch(GetNewReleasedBooks());
    dispatch(GetBestSellersgBooks());
    dispatch(GetRecommendedBooks());
    dispatch(GetTopRatedBooks());
    if (IsLoggedIn) dispatch(GetCartAPI());
  }, [IsLoggedIn]);

  const handleShowCart = async (id: number) => {
    await dispatch(GetBookInfo(id));
    setShowAddToCartScreen(true);
  };

  async function HandleAddingTocart(product: GetProductInfo) {
     const item = CartItems.find((item) => item.productId === product.id);
      if (item&&item.totalQuantity>=product.quantity) {
        setAlert({ type: "error", message: "product is out of stock." });
        
      }
      else{

    const result = await AddProductToCartAPI(product.id, navigate);
    if (result) {
      const Item: CartItem = {
        id: result,
        productId: product.id,
        name: product.name,
        imageUrl: product.imageUrl,
        totalPrice: product.price,
        bookCopyQuantity:product.quantity,
        totalQuantity: 1,
        createdAt: new Date().toISOString()
      };
      dispatch(AddToCart(Item));
      setAlert({ type: "success", message: "Added to cart successfully!" });
    } else if (result === false) {
      dispatch(SetLoggedInState(false));
      setAlert({ type: "error", message: "You must be logged in to add to cart." });
    } else {
      setAlert({ type: "error", message: "Failed to add to cart." });
    }
  }
   setShowAddToCartScreen(false);
  }

  const renderBookSection = (title: string, books: Book[] | null) => {
    if (!books) return
    return (
      <div className="w-full space-y-3">
        <h2 className="text-xl sm:text-2xl font-bold">{title}</h2>
        <div className="flex gap-4 overflow-x-auto scroll-snap-x pb-2">
          {books.map((book) => (
            <div
              key={book.id}
              className="flex-shrink-0 scroll-snap-start w-[180px] sm:w-[180px] bg-white rounded-lg shadow hover:shadow-lg transition flex flex-col"
            >
              <img
                src={book.imageUrl.replace("http://", "https://")}
                onClick={() => {
                  SetBookIdToLocalStorage(book.id);
                  navigate("/Detail");
                }}
                alt={book.name}
                className="w-full h-[220px] sm:h-[240px] object-cover rounded-t cursor-pointer"
              />
              <div className="p-2 flex flex-col flex-grow">
                <h3 className="text-sm font-semibold line-clamp-2">{book.name}</h3>
                <h4 className="text-xs text-gray-500  mb-2">{book.authorName}</h4>
                <p className="text-sm font-bold text-gray-900 mb-2">
                  ${book.price}
                </p>
                <button
                  onClick={() => {
                    SetBookIdToLocalStorage(book.id);
                    handleShowCart(book.id);
                  }}
                  className="mt-auto bg-sky-600 text-white text-xs py-1.5 px-2 rounded hover:bg-sky-700 transition"
                >
                  Add to Cart
                </button>
              </div>
            </div>
          ))}
        </div>
        
      </div>
    );
  };

  return (
    <div className="relative">
      {alert && <SlideAlert type={alert.type} message={alert.message} onClose={() => setAlert(null)} />}

      <div className="px-4 sm:px-6 py-6 space-y-12 text-gray-800 ">
        <div className="flex flex-col gap-10">
          {renderBookSection("Recommended", RecommendedBooks)}
          {renderBookSection("Top Rated", TopRatedBooks)}
          {renderBookSection("Our Bestsellers", BestSellersBooks)}
          {renderBookSection("New Releases", NewBooks)}
        </div>
         
      </div>

      {showAddToCartScreen && Product && (
        <div className="fixed inset-0 z-50 backdrop-blur-sm flex items-center justify-center px-2 sm:px-4">
          <div className="relative bg-white rounded-2xl shadow-2xl w-full max-w-lg sm:max-w-2xl md:max-w-4xl p-4 sm:p-6 flex flex-col md:flex-row gap-6 sm:gap-8 overflow-hidden">
            
            <button
              onClick={() => setShowAddToCartScreen(false)}
              className="absolute top-3 right-3 bg-gray-200 text-gray-800 rounded-full p-2 hover:bg-red-500 hover:text-white transition"
              aria-label="Close"
            >
              ✕
            </button>

         
            <div className="w-full md:w-[320px] flex-shrink-0 flex justify-center">
              <img
                src={Product.imageUrl.replace("http://", "https://")}
                alt={Product.name}
                className="w-[200px] sm:w-[240px] md:w-[280px] lg:w-[320px] h-auto rounded-xl shadow object-cover"
              />
            </div>

         
            <div className="flex flex-col justify-between flex-grow">
              <div>
                <h1 className="text-lg sm:text-xl md:text-2xl font-bold mb-1">{Product.name}</h1>
                <h2 className="text-sm text-gray-600  mb-2">
                  by <span className="font-medium">{Product.author}</span>
                </h2>

                <div className="flex items-center gap-2 mb-3">
                  <span className="text-yellow-400 text-lg">★</span>
                  <span className="text-sm sm:text-md font-semibold">
                    {Product.averageRating.toFixed(1)}
                  </span>
                  <span className="text-xs text-gray-500">({Product.ratingsCount} ratings)</span>
                </div>

                <p className="text-sm text-gray-700 leading-relaxed max-h-[90px] overflow-y-auto pr-1">
                  {Product.description}
                </p>
              </div>

             
              <div className="space-y-2 mt-4">
                <div className="text-lg font-bold text-sky-600">${Product.price.toFixed(2)}</div>
                <div className="text-xs text-gray-600">
                  Quantity:{" "}
                  <span className={Product.quantity > 0 ? "text-green-600" : "text-red-500"}>
                    {Product.quantity > 0 ? Product.quantity : "Out of Stock"}
                  </span>
                </div>

                <button
                  disabled={Product.quantity === 0}
                  onClick={() => {
                    if (IsLoggedIn === false) {
                      navigate("/Login");
                      return;
                    }
                    HandleAddingTocart(Product);
                  }}
                  className="w-full bg-sky-600 text-white text-sm py-2 rounded-xl hover:bg-sky-700 transition disabled:opacity-50"
                >
                  Add to Cart
                </button>
              </div>
            </div>
          </div>
        </div>
      )}
     
    </div>
  );
}
