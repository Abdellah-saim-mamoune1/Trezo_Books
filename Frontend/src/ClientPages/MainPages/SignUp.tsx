import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { ISignUp } from "../../Interfaces/ClientInterfaces";
import { SignUpAPI, GetClientInfoAPI } from "../../APIs/ClientAPIs";
import { useAppDispatch } from "../../Slices/Hooks";
import { SetLoggedInState } from "../../Slices/ClientSlices/ClientInfoSlice";
import { SetUserType } from "../../Slices/EmployeeSlices/EmployeeInfoSlice";

export function SignUp() {
  const [form, setForm] = useState({
    firstName: "",
    lastName: "",
    account: "",
    password: "",
    phone: "",
  });
  const dispatch = useAppDispatch();
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);

  const navigate = useNavigate();

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  

  const handleSubmit = async () => {
    const { firstName, lastName, account, password, phone } = form;
    setError("");

    if (!firstName || !lastName || !account || !password || !phone) {
      setError("All fields are required.");
      return;
    }



    try {
      setLoading(true);

      // Prepare data for your backend
      const data: ISignUp = {
        firstName,
        lastName,
        phoneNumber: phone,
        account_informations: {
          account,
          password,
        },
      };

      const result = await SignUpAPI(data);

      if (result !== false) {
        await dispatch(GetClientInfoAPI());
        dispatch(SetUserType("Client"));
        dispatch(SetLoggedInState(true));
        navigate("/");
      } else {
        setError("Sign up failed. Please try again.");
      }
    } catch (err: any) {
      console.error(err);
      setError(err.message || "Sign up failed.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="w-full h-full flex items-center justify-center text-gray-800">
      <div className="bg-white p-8 rounded shadow-md w-full max-w-md">
        <h2 className="text-2xl font-bold text-center mb-6">Sign Up</h2>

        {error && <div className="mb-4 text-red-500 text-sm">{error}</div>}

        <div className="mb-4">
          <label className="block text-sm mb-1">First Name</label>
          <input
            name="firstName"
            value={form.firstName}
            onChange={handleChange}
            required
            className="w-full px-3 py-2 border rounded bg-white"
          />
        </div>

        <div className="mb-4">
          <label className="block text-sm mb-1">Last Name</label>
          <input
            name="lastName"
            value={form.lastName}
            onChange={handleChange}
            required
            className="w-full px-3 py-2 border rounded bg-white"
          />
        </div>

        <div className="mb-4">
          <label className="block text-sm mb-1">Email (Account)</label>
          <input
            name="account"
            type="email"
            required
            value={form.account}
            onChange={handleChange}
            className="w-full px-3 py-2 border rounded bg-white"
          />
        </div>

        <div className="mb-4">
          <label className="block text-sm mb-1">Password</label>
          <input
            name="password"
            type="password"
            required
            value={form.password}
            onChange={handleChange}
            className="w-full px-3 py-2 border rounded bg-white"
          />
        </div>

        <div className="mb-6">
          <label className="block text-sm mb-1">Phone Number</label>
          <input
            name="phone"
            type="tel"
            required
            maxLength={15}
            value={form.phone}
            onChange={handleChange}
            className="w-full px-3 py-2 border rounded bg-white"
          />
        </div>

        <button
          onClick={handleSubmit}
          disabled={loading}
          className="w-full bg-sky-600 hover:bg-sky-700 text-white font-semibold py-2 px-4 rounded transition disabled:opacity-50"
        >
          {loading ? "Signing up..." : "Sign Up"}
        </button>
      </div>
    </div>
  );
}
