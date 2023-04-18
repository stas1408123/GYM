import { Link } from "react-router-dom";

export function Navigation() {
  return (
    <nav className="h-[50px] flex justify-between px-5 bg-slate-800 items-center text-white">
      <span className="font-bold">GYM</span>

      <span>
        <Link to="/" className="mr-2">
          Products
        </Link>
        <Link to="/coaches" className="mr-2">
          Coaches
        </Link>

        <Link to="/about">About</Link>
      </span>
    </nav>
  );
}
