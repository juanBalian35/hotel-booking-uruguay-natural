import DateTimeFormat = Intl.DateTimeFormat;

export interface SearchLodging {
    checkIn: string;
    checkOut: string;
    retirees: number;
    adults: number;
    babies: number;
    children: number;
    touristSpot: number;
    page: number;
    resultsPerPage: number;
    isFull: boolean;
}
