import axios from "axios";
import { IPagination } from "../Interfaces/PublicInterfaces";
import { IAddEmployee, IAddNewBookCopy,IGetBook,IPerson,IUpdateBookCopy } from "../Interfaces/EmployeeInterfaces";
import { createAsyncThunk } from "@reduxjs/toolkit";
import { SetEmployeeLoggedInState, SetUserType } from "../Slices/EmployeeSlices/EmployeeInfoSlice";
import { SetUserTypeToLocalStorage } from "../Utilities/UClient";
import { IRestPassword } from "../Interfaces/ClientInterfaces";

export async function  GetTotalsAPI(){
    try{
      const response=await axios.get(`https://mybackendecommerce-app-argfascphqaedvaq.spaincentral-01.azurewebsites.net/api/GetDashBoardStatistics/GetTotals/`,{
      withCredentials: true,
    });
    return response.data.data;

  } catch (error: any) {
   
   return false;
  }
}


export async function  GetResentOrders(){
    try{
      const response=await axios.get(`https://mybackendecommerce-app-argfascphqaedvaq.spaincentral-01.azurewebsites.net/api/GetDashBoardStatistics/GetResentOrders/`,{
      withCredentials: true,
    });
    return response.data.data;

  } catch (error: any) {
   
   return false;
  }
}

export async function  GetNewClients(){
    try{
      const response=await axios.get(`https://mybackendecommerce-app-argfascphqaedvaq.spaincentral-01.azurewebsites.net/api/GetDashBoardStatistics/GetNewClients/`,{
      withCredentials: true,
    });
    return response.data.data;

  } catch (error: any) {
  
   return false;
  }
}

export async function  GetAllClients(){
    try{
      const response=await axios.get(`https://mybackendecommerce-app-argfascphqaedvaq.spaincentral-01.azurewebsites.net/api/GetDashBoardStatistics/GetAllClients/`,{
      withCredentials: true,
    });
    return response.data.data;

  } catch (error: any) {
   
   return false;
  }
}


export async function  GetPaginatedAuthors(pagination:IPagination){
    try{
      const response=await axios.get(`https://mybackendecommerce-app-argfascphqaedvaq.spaincentral-01.azurewebsites.net/api/AuthorManagement/GetPaginatedAuthors/${pagination.pageNumber},${pagination.pageSize}`,{
      withCredentials: true,
    });
    return response.data.data;

  } catch (error: any) {

   return false;
  }
}

export async function  GetPaginatedBooks(pagination:IPagination){
    try{
      const response=await axios.get(`https://mybackendecommerce-app-argfascphqaedvaq.spaincentral-01.azurewebsites.net/api/BookManagement/GetPaginatedBooks/${pagination.pageNumber},${pagination.pageSize}`,{
      withCredentials: true,
    });
    return response.data.data;

  } catch (error: any) {
  
   return false;
  }
}




export async function  AddBook(data:string){
    try{
      const response=await axios.post(`https://mybackendecommerce-app-argfascphqaedvaq.spaincentral-01.azurewebsites.net/api/BookManagement/AddNewBook/${data}`,{},{
      withCredentials: true,
    });
    return response.data.data;

  } catch (error: any) {
   console.log(error);
   return false;
  }
}

export async function  DeleteBook(Id:Number){
    try{
      const response=await axios.delete(`https://mybackendecommerce-app-argfascphqaedvaq.spaincentral-01.azurewebsites.net/api/BookManagement/DeleteBook/${Id}`,{
      withCredentials: true,
    });
    return response.data.data;

  } catch (error: any) {
   
   return false;
  }
}

export async function  UpdateBook(data:IGetBook){
    try{
      const response=await axios.put(`https://mybackendecommerce-app-argfascphqaedvaq.spaincentral-01.azurewebsites.net/api/BookManagement/UpdateBook/`,data,{
      withCredentials: true,
    });
    return response.data.data;

  } catch (error: any) {
 
   return false;
  }
}


export async function  UpdateAuthor(Id:number,Name:string){
    try{
      const response=await axios.put(`https://mybackendecommerce-app-argfascphqaedvaq.spaincentral-01.azurewebsites.net/api/AuthorManagement/UpdateAuthor/${Id},${Name}`,{},{
      withCredentials: true,
    });
    return response.data.data;

  } catch (error: any) {
  
   return false;
  }
}

export async function  AddNewAuthor(Name:string){
    try{
      const response=await axios.post(`https://mybackendecommerce-app-argfascphqaedvaq.spaincentral-01.azurewebsites.net/api/AuthorManagement/AddNewAuthor/${Name}`,{},{
      withCredentials: true,
    });
    return response.data.data;

  } catch (error: any) {
  
   return false;
  }
}


export async function  DeleteAuthor(Id:number){
    try{
      const response=await axios.delete(`https://mybackendecommerce-app-argfascphqaedvaq.spaincentral-01.azurewebsites.net/api/AuthorManagement/DeleteAuthor/${Id}`,{
      withCredentials: true,
    });
    return response.data.data;

  } catch (error: any) {
   
   return false;
  }
}



export async function  GetPaginatedBookCopies(Pagination:IPagination){
    try{
      const response=await axios.get(`https://mybackendecommerce-app-argfascphqaedvaq.spaincentral-01.azurewebsites.net/api/BookCopiesManagement/GetPaginatedBooksCopies/${Pagination.pageNumber},${Pagination.pageSize}`,{
      withCredentials: true,
    });
    return response.data.data;

  } catch (error: any) {
 
   return false;
  }
}

export async function  DeleteBookCopy(Id:number){
    try{
      const response=await axios.delete(`https://mybackendecommerce-app-argfascphqaedvaq.spaincentral-01.azurewebsites.net/api/BookCopiesManagement/DeleteBookCopy/${Id}`,{
      withCredentials: true,
    });
    return response.data.data;

  } catch (error: any) {
  
   return false;
  }
}



export async function AddNewBookCopy(data:IAddNewBookCopy){
    try{
      const response=await axios.post(`https://mybackendecommerce-app-argfascphqaedvaq.spaincentral-01.azurewebsites.net/api/BookCopiesManagement/AddNewBookCopy/`,data,{
      withCredentials: true,
    });
    return response.data.data;

  } catch (error: any) {
   
   return false;
  }
}

export async function UpdateBookCopy(data:IUpdateBookCopy){
    try{
      const response=await axios.put(`https://mybackendecommerce-app-argfascphqaedvaq.spaincentral-01.azurewebsites.net/api/BookCopiesManagement/UpdateBookCopy/`,data,{
      withCredentials: true,
    });
    return response.data.data;

  } catch (error: any) {
  
   return false;
  }
}




export async function GetAllOrders(pagination:IPagination){
    try{
      const response=await axios.get(`https://mybackendecommerce-app-argfascphqaedvaq.spaincentral-01.azurewebsites.net/api/OrderManagementByEmployee/GetOrders/${pagination.pageNumber},${pagination.pageSize}`,{
      withCredentials: true,
    });
    return response.data.data;

  } catch (error: any) {
   
   return false;
  }
}

export async function SetOrderStatus(OrderId:number,Status:string){
    try{
      const response=await axios.put(`https://mybackendecommerce-app-argfascphqaedvaq.spaincentral-01.azurewebsites.net/api/OrderManagementByEmployee/SetOrderStatus/${OrderId},${Status}`,{},{
      withCredentials: true,
    });
    return response.data.data;

  } catch (error: any) {
   
   return false;
  }
}





export async function GetAllEmployees(){
    try{
      const response=await axios.get(`https://mybackendecommerce-app-argfascphqaedvaq.spaincentral-01.azurewebsites.net/api/EmployeeManagement/GetAllEmployees/`,{
      withCredentials: true,
    });
    return response.data.data;

  } catch (error: any) {
   
   return false;
  }
}


export async function AddNewEmployee(data:IAddEmployee){
    try{
      const response=await axios.post(`https://mybackendecommerce-app-argfascphqaedvaq.spaincentral-01.azurewebsites.net/api/EmployeeManagement/RegisterEmployee/`,data,{
      withCredentials: true,
    });
    return response.data.data;

  } catch (error: any) {
   console.log(error);
   return false;
  }
}


export async function DeleteEmployee(Id:number){
    try{
      const response=await axios.delete(`https://mybackendecommerce-app-argfascphqaedvaq.spaincentral-01.azurewebsites.net/api/EmployeeManagement/DeleteEmployee/${Id}`,{
      withCredentials: true,
    });
    return response.data.data;

  } catch (error: any) {
   
   return false;
  }
}

export async function ResetPassword(data:IRestPassword){
    try{
      const response=await axios.put(`https://mybackendecommerce-app-argfascphqaedvaq.spaincentral-01.azurewebsites.net/api/EmployeeManagement/ResetPassword/`,data,
         { withCredentials: true}
      );
    return response.data.data;

  } catch (error: any) {
   
   return false;
  }
}

export async function UpdatePersonlaInfoAPI(data:IPerson){
    try{
      const response=await axios.put(`https://mybackendecommerce-app-argfascphqaedvaq.spaincentral-01.azurewebsites.net/api/EmployeeManagement/UpdateEmployeeInfo/`,data,
       { withCredentials: true}
      );
    return response.data.data;

  } catch (error: any) {
   
   return false;
  }
}

export async function GetClientsMessagesAPI(){
    try{
      const response=await axios.get(`https://mybackendecommerce-app-argfascphqaedvaq.spaincentral-01.azurewebsites.net/api/ContactUsManagement/GetMessages/`,
       { withCredentials: true}
      );
    return response.data.data;

  } catch (error: any) {
   
   return false;
  }
}



export async function DeleteClientMessageAPI(Id:number){
    try{
      const response=await axios.delete(`https://mybackendecommerce-app-argfascphqaedvaq.spaincentral-01.azurewebsites.net/api/ContactUsManagement/DeleteMessage/${Id}`,
       { withCredentials: true}
      );
    return response.data.data;

  } catch (error: any) {
   
   return false;
  }
}

export async function SearchAuthorByIdAPI(Id:number){
    try{
      const response=await axios.get(`https://mybackendecommerce-app-argfascphqaedvaq.spaincentral-01.azurewebsites.net/api/AuthorManagement/GetAuthorById/${Id}`,
       { withCredentials: true}
      );
    return response.data.data;

  } catch (error: any) {
   
   return false;
  }
}

export async function SearchAuthorByNameAPI(Name:string){
    try{
      const response=await axios.get(`https://mybackendecommerce-app-argfascphqaedvaq.spaincentral-01.azurewebsites.net/api/AuthorManagement/GetAuthorByName/${Name}`,
       { withCredentials: true}
      );
    return response.data.data;

  } catch (error: any) {
   
   return false;
  }
}

export async function SearchBookByIdAPI(Id:number){
    try{
      const response=await axios.get(`https://mybackendecommerce-app-argfascphqaedvaq.spaincentral-01.azurewebsites.net/api/BookManagement/GetBookById/${Id}`,
       { withCredentials: true}
      );
    return response.data.data;

  } catch (error: any) {
   
   return false;
  }
}


export async function SearchBookByNameAPI(Name:string){
    try{
      const response=await axios.get(`https://mybackendecommerce-app-argfascphqaedvaq.spaincentral-01.azurewebsites.net/api/BookManagement/GetBookByName/${Name}`,
       { withCredentials: true}
      );
     
    return response.data.data;

  } catch (error: any) {
   
   return false;
  }
}

export async function SearchBookCopyByIdAPI(Id:number){
    try{
      const response=await axios.get(`https://mybackendecommerce-app-argfascphqaedvaq.spaincentral-01.azurewebsites.net/api/BookCopiesManagement/GetBookCopyById/${Id}`,
       { withCredentials: true}
      );
      
    return response.data.data;

  } catch (error: any) {
   
   return false;
  }
}



export const GetEmployeeInfoAPI= createAsyncThunk(
  'Employee/GetEmployeeInfoAPI',
  async () => {
    
    try{
      const response=await axios.get(`https://mybackendecommerce-app-argfascphqaedvaq.spaincentral-01.azurewebsites.net/api/EmployeeManagement/GetEmployeeInfo/`,{
      withCredentials: true,
    });
    
    return response.data.data;

  } catch (error: any) {
    
  }
  
  return null;}
);



export const RefreshEmployeeTokens= createAsyncThunk(
    'Cart/GetCart',
  async (_,thunkAPI) => {
    
  try {
    
    const Role=localStorage.getItem("UserType");
    console.log(Role);
    await axios.post(`https://mybackendecommerce-app-argfascphqaedvaq.spaincentral-01.azurewebsites.net/api/Authorization/refresh-tokens/${Role}`,{}, {
      withCredentials: true,
    });
    thunkAPI.dispatch(SetEmployeeLoggedInState(true));
    
    return true;
  } catch (error: any) {
    thunkAPI.dispatch(SetEmployeeLoggedInState(false));
    thunkAPI.dispatch(SetUserType("Client"));
    SetUserTypeToLocalStorage("Client");

    return false;
  }
})
