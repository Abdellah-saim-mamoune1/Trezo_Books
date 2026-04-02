import { useEffect, useState } from "react";
import { AddNewAuthor, DeleteAuthor, GetPaginatedAuthors, SearchAuthorByIdAPI, SearchAuthorByNameAPI, UpdateAuthor } from "../APIs/EmployeeAPIs";
import { IPagination } from "../Interfaces/PublicInterfaces";
import { IAuthor } from "../Interfaces/EmployeeInterfaces";

type AuthorResponse = {
  authors: IAuthor[];
  totalPages: number;
};

export function Authors() {
  const [authors, setAuthors] = useState<IAuthor[]>([]);
  const [searchTerm, setSearchTerm] = useState("");
  const [pagination, setPagination] = useState<IPagination>({
    pageNumber: 1,
    pageSize: 10,
  });

  const [newAuthor, setNewAuthor] = useState<string>("");

  const handleAddAuthor = async () => {
    if (newAuthor.trim() === "") {
      setMessage({ type: "error", text: "Author name is required." });
      return;
    }

    const result = await AddNewAuthor({ fullName: newAuthor });
    if (result !== false) {
      setMessage({ type: "success", text: "Author added successfully." });
      setNewAuthor("");
      fetchAuthors();
    } else {
      setMessage({ type: "error", text: "Failed to add author." });
    }
  };

  const [totalPages, setTotalPages] = useState(1);
  const [loading, setLoading] = useState(true);
  const [message, setMessage] = useState<{
    type: "success" | "error";
    text: string;
  } | null>(null);

  const [editingId, setEditingId] = useState<number | null>(null);
  const [editedAuthor, setEditedAuthor] = useState<IAuthor | null>(null);

  const fetchAuthors = async () => {
    setLoading(true);
    const result: AuthorResponse | false = await GetPaginatedAuthors(pagination);
    if (result && result.authors) {
      setAuthors(result.authors);
      setTotalPages(result.totalPages);
    }
    setLoading(false);
  };

  useEffect(() => {
    fetchAuthors();
  }, [pagination]);

  useEffect(() => {
    if (message) {
      const timer = setTimeout(() => setMessage(null), 3000);
      return () => clearTimeout(timer);
    }
  }, [message]);

  const handleDelete = async (id: number) => {
    if (!confirm("Are you sure you want to delete this author?")) return;
    const result = await DeleteAuthor(id);
    if (result !== false) {
      setAuthors((prev) => prev.filter((a) => a.id !== id));
      setMessage({ type: "success", text: "Author deleted successfully." });
    } else {
      setMessage({ type: "error", text: "Failed to delete author." });
    }
  };

  const handleSearch = async () => {
    const term = searchTerm.trim();
    if (!term) {
      fetchAuthors();
      return;
    }

    setLoading(true);
    let result: IAuthor | IAuthor[] | false | null = false;

    if (!isNaN(Number(term))) {
      result = await SearchAuthorByIdAPI(Number(term));
      if (result !== false && result !== null && !Array.isArray(result)) setAuthors([result]);
    } else {
      result = await SearchAuthorByNameAPI(term);
      if (Array.isArray(result)) setAuthors(result);
    }

    if (!result) {
      setAuthors([]);
      setMessage({ type: "error", text: "No author found." });
    }

    setLoading(false);
  };

  const handleUpdate = async () => {
    if (!editedAuthor) return;
    const result = await UpdateAuthor(editedAuthor);
    if (result !== false) {
      setMessage({ type: "success", text: "Author updated successfully." });
      setEditingId(null);
      setEditedAuthor(null);
      fetchAuthors();
    } else {
      setMessage({ type: "error", text: "Failed to update author." });
    }
  };

  return (
    <div className="p-6">
      <h2 className="text-2xl font-semibold mb-4 text-gray-800">Authors</h2>

      {message && (
        <div className={`mb-4 px-4 py-2 rounded text-sm ${
            message.type === "success" ? "bg-green-100 text-green-700" : "bg-red-100 text-red-700"
          }`}>
          {message.text}
        </div>
      )}

      {loading ? (
        <div className="text-gray-600">Loading...</div>
      ) : (
        <>
          <div className="mb-6 bg-white p-4 rounded bg-gray-50">
            <h3 className="text-lg font-medium mb-2 text-gray-800">Add New Author</h3>
            <div className="grid grid-cols-1 items-center sm:grid-cols-2 gap-4">
              <input
                type="text"
                placeholder="Author Full Name"
                value={newAuthor}
                onChange={(e) => setNewAuthor(e.target.value)}
                className="p-2 border rounded"
              />
            </div>
            <button
              onClick={handleAddAuthor}
              className="mt-4 px-4 py-2 bg-green-600 text-white rounded hover:bg-green-700"
            >
              Add Author
            </button>
          </div>

          <div className="mb-6 flex flex-col bg-white p-5 rounded shadow sm:flex-row gap-2 sm:items-center">
            <input
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
              placeholder="Search by ID or Name"
              className="px-3 py-2 border rounded"
            />
            <button onClick={handleSearch} className="px-4 py-2 bg-blue-600 text-white rounded">
              Search
            </button>
            <button onClick={() => { setSearchTerm(""); fetchAuthors() }} className="px-4 py-2 bg-gray-600 text-white rounded">
              Reset
            </button>
          </div>

          <div className="overflow-x-auto">
            <table className="w-full overflow-x-auto border-collapse border border-gray-300">
              <thead>
                <tr className="bg-gray-100 text-gray-700">
                  <th className="p-2 border">ID</th>
                  <th className="p-2 border">Full Name</th>
                  <th className="p-2 border">Actions</th>
                </tr>
              </thead>
              <tbody>
                {authors.map((author) => (
                  <tr key={author.id} className="text-center border-t">
                    <td className="p-2 border">{author.id}</td>
                    <td className="p-2 border">
                      {editingId === author.id ? (
                        <input
                          value={editedAuthor?.fullName || ""}
                          onChange={(e) => setEditedAuthor((prev) => prev && { ...prev, fullName: e.target.value })}
                          className="px-2 py-1 rounded border w-full"
                        />
                      ) : (
                        author.fullName
                      )}
                    </td>
                    <td className="p-2 border space-x-2">
                      {editingId === author.id ? (
                        <>
                          <button onClick={handleUpdate} className="px-3 py-1 bg-blue-600 text-white rounded">
                            Save
                          </button>
                          <button
                            onClick={() => {
                              setEditingId(null);
                              setEditedAuthor(null);
                            }}
                            className="px-3 py-1 bg-gray-500 text-white rounded"
                          >
                            Cancel
                          </button>
                        </>
                      ) : (
                        <>
                          <button
                            onClick={() => {
                              setEditingId(author.id);
                              setEditedAuthor({ ...author });
                            }}
                            className="px-3 py-1 bg-yellow-500 text-white rounded"
                          >
                            Edit
                          </button>
                          <button
                            onClick={() => handleDelete(author.id)}
                            className="px-3 py-1 mt-3 bg-red-600 text-white rounded"
                          >
                            Delete
                          </button>
                        </>
                      )}
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>

          {/* Pagination */}
          <div className="mt-6 flex items-center justify-between text-sm text-gray-700">
            <button
              disabled={pagination.pageNumber <= 1}
              onClick={() =>
                setPagination((prev) => ({
                  ...prev,
                  pageNumber: Math.max(1, prev.pageNumber - 1),
                }))
              }
              className="px-3 py-1 bg-gray-200 rounded disabled:opacity-50"
            >
              Previous
            </button>
            <span>
              Page {pagination.pageNumber} of {totalPages}
            </span>
            <button
              disabled={pagination.pageNumber >= totalPages}
              onClick={() =>
                setPagination((prev) => ({
                  ...prev,
                  pageNumber: Math.min(totalPages, prev.pageNumber + 1),
                }))
              }
              className="px-3 py-1 bg-gray-200 rounded disabled:opacity-50"
            >
              Next
            </button>
          </div>
        </>
      )}
    </div>
  );
}