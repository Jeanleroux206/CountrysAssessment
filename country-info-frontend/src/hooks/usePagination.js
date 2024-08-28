import { useState } from 'react';

const usePagination = (perPage) => {
    const [currentPage, setCurrentPage] = useState(1);
    const [itemsPerPage, setItemsPerPage] = useState(perPage);

    const paginate = (pageNumber) => setCurrentPage(pageNumber);

    return { currentPage, itemsPerPage, setItemsPerPage, paginate };
};

export default usePagination;