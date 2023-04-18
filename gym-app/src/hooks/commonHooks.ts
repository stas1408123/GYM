import { useState } from "react";

interface ICustomError {
     [key:string] : string
}

export function CommonHooks () {
     const [loading, setLoading] = useState(false)
     const [error, setError] = useState('')

     return {loading,setLoading,error,setError}
}

export function ErrorsHook () {
     const[errors, setErrors] = useState<ICustomError[]>([])
     return {errors, setErrors}
}