import Navbar from "./navbar";

function LayoutBase(params) {
    return (
        <>
            <Navbar/>

            <section className="flex">
                {params.content}
            </section>


        </>
    )
}

export default LayoutBase;